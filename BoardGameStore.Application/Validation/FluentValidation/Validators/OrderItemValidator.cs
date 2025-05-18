using BoardGameStore.Application.DTOs.OrderDTOs;
using FluentValidation;

namespace BoardGameStore.Application.Validation.FluentValidation.Validators
{
    public class OrderItemValidator : AbstractValidator<AddOrderItemDTO>
    {
        public OrderItemValidator()
        {
            RuleFor(x => x.Quantity)
                .InclusiveBetween(1, 100);

            RuleFor(x => x.BoardGameId)
                .GreaterThan(0);
        }
    }
}
