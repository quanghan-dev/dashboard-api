using Core.Entities;
using DataAccess.Persistence;

namespace DataAccess.Repositories.Impl
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(DashboardContext context) : base(context) { }
    }
}