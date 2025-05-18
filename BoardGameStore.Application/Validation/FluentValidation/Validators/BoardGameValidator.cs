using BoardGameStore.Application.DTOs.BoardGameDTOs;
using FluentValidation;

namespace BoardGameStore.Application.Validation.FluentValidation.Validators
{
    public class BoardGameValidator : AbstractValidator<AddBoardGameDTO>
    {
        public BoardGameValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);

            RuleFor(x => x.Year)
                .InclusiveBetween(1900, DateTime.Now.Year);

            RuleFor(x => x.MinPlayers)
                .InclusiveBetween(1, 50);

            RuleFor(x => x.MaxPlayers)
                .InclusiveBetween(1, 50)
                .GreaterThanOrEqualTo(x => x.MinPlayers)
                .WithMessage("MaxPlayers must be greater than or equal to MinPlayers.");

            RuleFor(x => x.Difficulty)
                .IsInEnum();

            RuleFor(x => x.AvailableQuantity)
                .InclusiveBetween(0, 1000);

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0.01m);
        }
    }
}
