using Application.Models;
using Application.Models.Tokens;

namespace Application.Services
{
    public interface ITokenService
    {
        /// <summary>
        /// Create Token
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Refresh & access token</returns>
        public Task<ApiResult<TokenDto>> CreateToken(Guid userId);

        /// <summary>
        /// Logout
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns>Logout meessage</returns>
        public Task<ApiResult<string>> Logout(string accessToken);

        /// <summary>
        /// Refresh Token
        /// </summary>
        /// <param name="tokenDto"></param>
        /// <returns>Refresh & access token</returns>
        public Task<ApiResult<string>> RefreshToken(TokenDto tokenDto);
    }
}