using Core.Entities;
using DataAccess.Persistence;

namespace DataAccess.Repositories.Impl
{
    public class TokenRepository : Repository<Token>, ITokenRepository
    {
        public TokenRepository(DashboardContext context) : base(context) { }
    }
}