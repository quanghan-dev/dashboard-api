using Application.Models.Accounts;
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
                .WithMessage("Email address is not valid")
            .Must((acc, cancellationToken) =>
            {
                return _unitOfWork.Accounts.FindAccountByEmail(acc.Email) == null;
            })
            .WithMessage("Email address is already in use");

            RuleFor(acc => acc.Username)
                .NotEmpty()
                .WithMessage("Username is not valid")
            .Must((acc, cancellationToken) =>
            {
                return _unitOfWork.Accounts.FindAccountByUsername(acc.Username) == null;
            })
            .WithMessage("Username is already in use");

            RuleFor(acc => acc.Password)
                .MinimumLength(8)
                .WithMessage("Password should have minimum 8 characters");
        }
    }
}