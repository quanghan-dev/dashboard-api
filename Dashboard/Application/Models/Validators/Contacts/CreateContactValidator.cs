using Application.Models.Contacts;
using Core.Enums;
using FluentValidation;

namespace Application.Models.Validators.Contacts
{
    public class CreateContactValidator : AbstractValidator<CreateContactRequest>
    {
        public CreateContactValidator()
        {
            RuleFor(contact => contact.FirstName)
            .NotEmpty()
            .WithMessage(Message.GetMessage(ValidatorMessage.Invalid_Name));

            RuleFor(contact => contact.LastName)
            .NotEmpty()
            .WithMessage(Message.GetMessage(ValidatorMessage.Invalid_Name));

            RuleFor(contact => contact.Title)
            .NotEmpty()
            .WithMessage(Message.GetMessage(ValidatorMessage.Invalid_Title));

            RuleFor(contact => contact.Department)
            .NotEmpty()
            .WithMessage(Message.GetMessage(ValidatorMessage.Invalid_Department));

            RuleFor(contact => contact.Project)
            .NotEmpty()
            .WithMessage(Message.GetMessage(ValidatorMessage.Invalid_Project));
        }
    }
}