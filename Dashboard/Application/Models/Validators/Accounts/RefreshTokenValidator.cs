using Application.Models.Tokens;
using Core.Enums;
using FluentValidation;

namespace Application.Models.Validators.Accounts
{
    public class RefreshTokenValidator : AbstractValidator<TokenDto>
    {
        public RefreshTokenValidator()
        {
            RuleFor(token => token.AccessToken)
            .NotEmpty()
            .WithMessage(Message.GetMessage(ErrorMessage.Invalid_Token));

            RuleFor(token => token.RefreshToken)
            .NotEmpty()
            .WithMessage(Message.GetMessage(ErrorMessage.Invalid_Token));
        }
    }
}