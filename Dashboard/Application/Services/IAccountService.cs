using Application.Models;
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
        Task<ApiResult<string>> RegisterAccount(RegisterAccountRequest request);

        /// <summary>
        /// Activate Account
        /// </summary>
        /// <param name="code"></param>
        /// <returns>Result</returns>
        Task<ApiResult<string>> ActivateAccount(string code);

        /// <summary>
        /// Get User Id
        /// </summary>
        /// <param name="account"></param>
        /// <returns>User Id</returns>
        Guid GetUserId(LoginAccount account);
    }
}