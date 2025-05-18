using BoardGameStore.Application.DTOs.UserDTOs;
using FluentValidation;

namespace BoardGameStore.Application.Validation.FluentValidation.Validators
{
    public class AddressValidator : AbstractValidator<AddAddressDTO>
    {
        public AddressValidator() 
        {
            RuleFor(x => x.City)
                .NotEmpty()
                .Length(2, 30)
                .Matches(@"^[A-Za-z\s-]+$")
                .WithMessage("City can contain only letters, spaces and hyphens.");

            RuleFor(x => x.AddressLine)
                .NotEmpty()
                .MaximumLength(50)
                .Matches(@"^[A-Za-z\d\s\-./]+$")
                .WithMessage("Address line can contain only letters, numbers, spaces, hyphens, dots and slashes.");

            RuleFor(x => x.PostalCode)
                .NotEmpty()
                .Matches(@"^\d{2}-\d{3}$")
                .WithMessage("Invalid postal code format. Valid format: xx-yyy.");
        }
    }
}
