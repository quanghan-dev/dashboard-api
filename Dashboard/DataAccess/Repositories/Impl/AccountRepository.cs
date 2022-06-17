using Core.Entities;
using DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.Impl
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(DashboardContext context) : base(context) { }

        /// <summary>
        /// Find Account By Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Account</returns>
        public Account FindAccountByEmail(string? email)
        {
            return (_context.Accounts
                        .Where(acc => acc.Email!.Equals(email))
                        .FirstOrDefault())!;
        }

        /// <summary>
        /// Find Account By Username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Account</returns>
        public Account FindAccountByUsername(string? username)
        {
            return (_context.Accounts
                    .Where(acc => acc.Username!.Equals(username))
                    .FirstOrDefault())!;
        }
    }
}