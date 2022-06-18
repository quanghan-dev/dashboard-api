using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Models.Tokens;
using AutoMapper;
using Core.Entities;
using DataAccess.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services.Impl {
  public class TokenService : ITokenService
    {
        private readonly IUnitOfWork _unitOfWork = null!;
        private readonly IMapper _mapper = null!;
        private readonly byte[] _key;

        public TokenService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _key = Encoding.ASCII.GetBytes(configuration.GetValue<string>("Jwt:Custom:Key"));
        }


        public async Task<CreateTokenResponse> CreateToken(Guid userId)
        {
            Token token;
            try
            {
                token = new Token
                {
                    AccessToken = CreateAccessToken(userId),
                    RefreshToken = Guid.NewGuid(),
                    UserId = userId,
                    CreatedDate = DateTime.UtcNow,
                    IsRevoked = false,
                    ExpiredDate = DateTime.UtcNow.AddMonths(1)
                };

                _unitOfWork.Tokens.Add(token);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception) { throw; }
            return _mapper.Map<CreateTokenResponse>(token);
        }

        /// <summary>
        /// Create Access Token
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Access token</returns>
        private string CreateAccessToken(Guid id)
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
    }
}