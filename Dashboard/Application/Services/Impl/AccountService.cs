using Application.Common.Email;
using Application.Models.Accounts;
using AutoMapper;
using Core.Entities;
using DataAccess.UnitOfWork;
using Serilog;

namespace Application.Services.Impl
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IUtilService _utilService;
        private readonly IEmailService _emailService;
        private const string SERVER_URL = "https://localhost:7285/auth";

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
        /// <returns>Activate Code</returns>
        public async Task<string> RegisterAccount(RegisterAccountRequest request)
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
            catch (Exception e)
            {
                Log.Error(e.Message);
                throw;
            }
            return "Please activate your account via the link sent to your email.";
        }
    }
}