using BoardGameStore.Application.DTOs.OrderDTOs;
using FluentValidation;

namespace BoardGameStore.Application.Validation.FluentValidation.Validators
{
    public class OrderValidator : AbstractValidator<AddOrderDTO>
    {
        public OrderValidator()
        {
            RuleFor(x => x.TotalPrice)
                .GreaterThanOrEqualTo(0.01m);

            RuleFor(x => x.UserId)
                .GreaterThan(0);

            RuleFor(x => x.Items)
                .NotNull()
                .Must(items => items.Count != 0)
                .WithMessage("Items must contain at least one item.");

            RuleForEach(x => x.Items)
                .SetValidator(new OrderItemValidator());
        }
    }
}
