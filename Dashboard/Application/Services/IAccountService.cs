using Application.Models.Accounts;

namespace Application.Services
{
    public interface IAccountService
    {
        /// <summary>
        /// Register Account
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Activate Code</returns>
        Task<string> RegisterAccount(RegisterAccountRequest request);
    }
}