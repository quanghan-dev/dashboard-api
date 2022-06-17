using Core.Entities;

namespace DataAccess.Repositories
{
    public interface IAccountRepository : IRepository<Account>
    {
        /// <summary>
        /// Find Account By Username
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Account</returns>
        Account FindAccountByUsername(string? username);

        /// <summary>
        /// Find Account By Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Account FindAccountByEmail(string? email);
    }
}