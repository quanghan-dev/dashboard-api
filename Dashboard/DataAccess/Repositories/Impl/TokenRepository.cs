using Core.Entities;
using DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Impl
{
    public class TokenRepository : Repository<Token>, ITokenRepository
    {
        public TokenRepository(DashboardContext context) : base(context) { }
    }
}