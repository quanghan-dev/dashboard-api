using Core.Entities;

namespace DataAccess.Repositories
{
    public interface ITokenRepository : IRepository<Token>
    {
        /// <summary>
        /// Get Token By Access Token
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns>Token</returns>
        Task<Token> GetTokenByAccessToken(string accessToken);
    }
}