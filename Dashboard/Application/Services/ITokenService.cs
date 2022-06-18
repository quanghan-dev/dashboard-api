using Application.Models.Tokens;

namespace Application.Services
{
    public interface ITokenService
    {
        /// <summary>
        /// Create Token
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Access & Refresh Token</returns>
        public Task<CreateTokenResponse> CreateToken(Guid userId);
    }
}