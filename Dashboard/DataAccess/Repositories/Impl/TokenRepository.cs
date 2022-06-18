using Core.Entities;
using DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Impl
{
    public class TokenRepository : Repository<Token>, ITokenRepository
    {
        public TokenRepository(DashboardContext context) : base(context) { }

        /// <summary>
        /// Get Token By Access Token
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns>Token</returns>
        public async Task<Token> GetTokenByAccessToken(string accessToken)
        {
            return (await _context.Tokens.Where(token => token.AccessToken!.Equals(accessToken))
                        .FirstOrDefaultAsync())!;
        }
    }
}