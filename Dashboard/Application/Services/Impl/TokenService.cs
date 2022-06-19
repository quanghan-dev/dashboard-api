using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Models;
using Application.Models.Tokens;
using AutoMapper;
using Core.Entities;
using Core.Enums;
using DataAccess.UnitOfWork;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.Impl
{
    public class TokenService : ITokenService
    {
        private readonly IUnitOfWork _unitOfWork = null!;
        private readonly IMapper _mapper = null!;
        private readonly byte[] _key;
        private readonly IDistributedCache _distributedCache;
        private readonly IUtilService _utilService;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public TokenService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration,
            IDistributedCache distributedCache, IUtilService utilService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("Jwt:Custom:Key"));
            _distributedCache = distributedCache;
            _utilService = utilService;

            _tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(_key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                RequireExpirationTime = false,
                ClockSkew = TimeSpan.Zero
            };
        }

        /// <summary>
        /// Create Token
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Token</returns>
        public async Task<ApiResult<TokenDto>> CreateToken(Guid userId)
        {
            Token token;
            try
            {
                //revoke old token
                List<Token> oldTokens = await _unitOfWork.Tokens.FindListAsync(t => t.UserId.Equals(userId));
                if (oldTokens.Any())
                {
                    foreach (var tk in oldTokens)
                    {
                        if (!tk.IsRevoked!.Value)
                        {
                            tk.IsRevoked = true;
                            tk.UpdatedDate = DateTime.UtcNow;

                            _unitOfWork.Tokens.Update(tk);
                            if (_distributedCache.GetString(tk.AccessToken) != null)
                                await _distributedCache.SetStringAsync(tk.AccessToken, true.ToString());
                        }
                    }
                }

                token = new Token
                {
                    AccessToken = CreateAccessToken(userId.ToString()),
                    RefreshToken = Guid.NewGuid(),
                    UserId = userId,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    IsRevoked = false,
                    ExpiredDate = DateTime.UtcNow.AddMonths(1)
                };

                _unitOfWork.Tokens.Add(token);
                await _unitOfWork.SaveChangesAsync();

                //store token to Redis
                _distributedCache.SetString(token.AccessToken, false.ToString(),
                    new DistributedCacheEntryOptions() { SlidingExpiration = TimeSpan.FromHours(6) });
            }
            catch (Exception) { throw; }
            return ApiResult<TokenDto>.Success(_mapper.Map<TokenDto>(token));
        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns>Logout message</returns>
        public async Task<ApiResult<string>> Logout(string accessToken)
        {
            try
            {
                Token token = await _unitOfWork.Tokens.GetTokenByAccessToken(accessToken);
                token.IsRevoked = true;
                token.ExpiredDate = DateTime.UtcNow;

                _unitOfWork.Tokens.Update(token);
                await _unitOfWork.SaveChangesAsync();

                await _distributedCache.SetStringAsync(accessToken, true.ToString());
            }
            catch (Exception) { throw; }
            return ApiResult<string>.Success("Logout Successfully");
        }

        /// <summary>
        /// Refresh Token
        /// </summary>
        /// <param name="tokenDto"></param>
        /// <returns>Refresh & access token</returns>
        public async Task<ApiResult<string>> RefreshToken(TokenDto tokenDto)
        {
            Token token;
            try
            {
                token = (await _unitOfWork.Tokens.FindAsync(t => t.RefreshToken.Equals(tokenDto.RefreshToken)))!;
                if (token is null)
                    throw new UnauthorizedAccessException(Message.GetMessage(ErrorMessage.Invalid_Token));

                token.AccessToken = VerifyAndGenerateAccessToken(tokenDto, token);
                token.UpdatedDate = DateTime.UtcNow;

                _unitOfWork.Tokens.Update(token);
                await _unitOfWork.SaveChangesAsync();

                //store token to Redis
                _distributedCache.SetString(token.AccessToken, false.ToString(),
                    new DistributedCacheEntryOptions() { SlidingExpiration = TimeSpan.FromHours(6) });
            }
            catch (System.Exception) { throw; }
            return ApiResult<string>.Success(_mapper.Map<TokenDto>(token).AccessToken!);
        }

        /// <summary>
        /// Create Access Token
        /// </summary>
        /// <param name="id"></param>
        /// <returns>access token</returns>
        private string CreateAccessToken(string id)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(6),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(_key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Verify And Generate Access Token
        /// </summary>
        /// <param name="tokenDto"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private string VerifyAndGenerateAccessToken(TokenDto tokenDto, Token token)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            DateTime expiredDate = DateTime.MinValue;

            try
            {
                //validation 1
                var tokenInVerification = jwtTokenHandler
                            .ValidateToken(tokenDto.AccessToken, _tokenValidationParameters, out var validatedToken);

                // Validation 2 - Validate encryption alg
                if (validatedToken is JwtSecurityToken jwtSecurityToken)
                {
                    var result = jwtSecurityToken.Header.Alg
                            .Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

                    if (result == false)
                        throw new UnauthorizedAccessException(Message.GetMessage(ErrorMessage.Invalid_Token));
                }

                // Validation 3 - validate expiry date of Access Token
                var utcExpiryDate = long.Parse(tokenInVerification.Claims
                                        .FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp)!.Value);

                if (_utilService.UnixTimeStampToDateTime(utcExpiryDate) > DateTime.UtcNow)
                    throw new UnauthorizedAccessException(Message.GetMessage(ErrorMessage.Invalid_Token));

                // Validation 4 - validate if Refresh Token is revoked
                if (token.IsRevoked is true)
                    throw new UnauthorizedAccessException(Message.GetMessage(ErrorMessage.Invalid_Token));

                // Validation 5 - validate User Id
                var userId = tokenInVerification.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)!.Value;

                if (!token.UserId.ToString().Equals(userId))
                    throw new UnauthorizedAccessException(Message.GetMessage(ErrorMessage.Invalid_Token));

                // Validation 6 - validate expiry date oof Refresh Token
                if (token.ExpiredDate < DateTime.UtcNow)
                    throw new UnauthorizedAccessException(Message.GetMessage(ErrorMessage.Invalid_Token));

                // update current token
                string accessToken = CreateAccessToken(userId);

                return accessToken;
            }
            catch (System.Exception) { throw; }
        }
    }
}