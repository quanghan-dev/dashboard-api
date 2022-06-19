using Application.Common.Email;
using Application.Exceptions;
using Application.Models;
using Application.Models.Accounts;
using AutoMapper;
using Core.Entities;
using Core.Enums;
using DataAccess.UnitOfWork;

namespace Application.Services.Impl
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUtilService _utilService;
        private readonly IEmailService _emailService;
        private const string SERVER_URL = "https://localhost:7285/auth/activation";

        public AccountService(IUnitOfWork unitOfWork, IMapper mapper,
        IUtilService utilService, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _utilService = utilService;
            _emailService = emailService;
        }

        /// <summary>
        /// Register Account
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<ApiResult<string>> RegisterAccount(RegisterAccountRequest request)
        {
            try
            {
                string activeCode = _utilService.RandomString();

                Account account = _mapper.Map<RegisterAccountRequest, Account>(request);
                account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
                account.IsActive = false;
                account.ActivateCode = activeCode;

                //send activate email
                EmailMessage message = new()
                {
                    ToAddress = account.Email,
                    Body = $"Activate account link: {SERVER_URL}?code={activeCode}",
                    Subject = "Activate Account"
                };
                await _emailService.SendEmailAsync(message);

                _unitOfWork.Accounts.Add(account);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception) { throw; }
            return ApiResult<string>.Success(Message.GetMessage(ServiceMessage.Activate_Message));
        }

        /// <summary>
        /// Activate Account
        /// </summary>
        /// <param name="code"></param>
        /// <returns>Account</returns>
        public async Task<ApiResult<string>> ActivateAccount(string code)
        {
            try
            {
                Account account = await _unitOfWork.Accounts.FindAccountByActivateCode(code);
                if (account == null)
                    throw new NotFoundException(Message.GetMessage(ServiceMessage.Invalid_Activate_Code));

                account.IsActive = true;

                _unitOfWork.Accounts.Update(account);
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception) { throw; }
            return ApiResult<string>.Success(Message.GetMessage(ServiceMessage.Activated_Account));
        }

        /// <summary>
        /// Get User Id
        /// </summary>
        /// <param name="account"></param>
        /// <returns>User Id</returns>
        public Guid GetUserId(LoginAccount account)
        {
            try
            {
                Account acc = _unitOfWork.Accounts.FindAccountByUsernameAndPassword(account.Username!, account.Password!);
                if (acc == null) throw new UnauthorizedAccessException(Message.GetMessage(ValidatorMessage.Invalid_Username_Password));
                return acc.UserId;
            }
            catch (System.Exception) { throw; }
        }
    }
}