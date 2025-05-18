using BoardGameStore.Application.DTOs.UserDTOs;
using FluentValidation;

namespace BoardGameStore.Application.Validation.FluentValidation.Validators
{
    public class UserValidator : AbstractValidator<AddUserDTO>
    {
        private const int minAge = 18;
        private const int maxAge = 100;

        public UserValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .Length(2, 30)
                .Matches(@"^[A-Za-z\s-']+$")
                .WithMessage("First name can contain only letters, spaces, hyphens and apostrophes.");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .Length(2, 30)
                .Matches(@"^[A-Za-z\s-']+$")
                .WithMessage("Last name can contain only letters, spaces, hyphens and apostrophes.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .MaximumLength(50)
                .EmailAddress();

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Matches(@"^\+?[0-9\s\-]{7,20}$")
                .WithMessage("Invalid phone number format.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty()
                .Must(date => IsValidAge(date, minAge, maxAge))
                .WithMessage($"Date of birth must be between {DateTime.Today.AddYears(-maxAge):dd-MM-yyyy} and {DateTime.Today.AddYears(-minAge):dd-MM-yyyy}.");

            When(x => x.Address != null, () =>
            {
                RuleFor(x => x.Address).SetValidator(new AddressValidator());
            });
        }

        private static bool IsValidAge(DateTime date, int minAge, int maxAge)
        {
            var minDate = DateTime.Today.AddYears(-maxAge);
            var maxDate = DateTime.Today.AddYears(-minAge);

            return date >= minDate && date <= maxDate;
        }
    }
}
