using Application.Models.Accounts;
using Core.Enums;
using DataAccess.UnitOfWork;
using FluentValidation;

namespace Application.Models.Validators
{
    public class RegisterAccountValidator : AbstractValidator<RegisterAccountRequest>
    {
        private readonly IUnitOfWork _unitOfWork;
        public RegisterAccountValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(acc => acc.Email)
                .EmailAddress()
                .WithMessage(Message.GetMessage(ValidatorMessage.Invalid_Email))
            .Must((acc, cancellationToken) =>
            {
                return _unitOfWork.Accounts.FindAccountByEmail(acc.Email) == null;
            })
            .WithMessage(Message.GetMessage(ValidatorMessage.Used_Email));

            RuleFor(acc => acc.Username)
                .NotEmpty()
                .WithMessage(Message.GetMessage(ValidatorMessage.Invalid_Username))
            .Must((acc, cancellationToken) =>
            {
                return _unitOfWork.Accounts.FindAccountByUsername(acc.Username) == null;
            })
            .WithMessage(Message.GetMessage(ValidatorMessage.Used_Username));

            RuleFor(acc => acc.Password)
                .MinimumLength(8)
                .WithMessage(Message.GetMessage(ValidatorMessage.Invalid_Password_Length));
        }
    }
}