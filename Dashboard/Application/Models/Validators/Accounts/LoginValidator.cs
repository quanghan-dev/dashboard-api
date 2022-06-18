using Application.Models.Accounts;
using Core.Enums;
using DataAccess.UnitOfWork;
using FluentValidation;

namespace Application.Models.Validators.Accounts
{
    public class LoginValidator : AbstractValidator<LoginAccount>
    {
        private readonly IUnitOfWork _unitOfWork;

        public LoginValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(acc => acc.Username)
                .NotEmpty()
                .WithMessage(Message.GetMessage(ValidatorMessage.Invalid_Username));

            RuleFor(acc => acc.Password)
                .MinimumLength(8)
                .WithMessage(Message.GetMessage(ValidatorMessage.Invalid_Password_Length))
                .Must((acc, cancellationToken) =>
                {
                    return _unitOfWork.Accounts.FindAccountByUsernameAndPassword(acc.Username!, acc.Password!) != null;
                })
                .WithMessage(Message.GetMessage(ValidatorMessage.Invalid_Username_Password));
        }
    }
}