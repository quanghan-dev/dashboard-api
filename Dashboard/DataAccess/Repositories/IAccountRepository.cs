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
        /// <returns>Account</returns>
        Account FindAccountByEmail(string? email);

        /// <summary>
        /// Find Account By Activate Code
        /// </summary>
        /// <param name="code"></param>
        /// <returns>Account</returns>
        Task<Account> FindAccountByActivateCode(string? code);

        /// <summary>
        /// Find Account By Username And Password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Account FindAccountByUsernameAndPassword(string username, string password);
    }
}