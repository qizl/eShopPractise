using EnjoyCodes.eShopOnContainers.Services.Ordering.API.Application.Commands;
using FluentValidation;

namespace EnjoyCodes.eShopOnContainers.Services.Ordering.API.Application.Validations
{
    public class ShipOrderCommandValidator : AbstractValidator<ShipOrderCommand>
    {
        public ShipOrderCommandValidator()
        {
            RuleFor(order => order.OrderNumber).NotEmpty().WithMessage("No orderId found");
        }
    }
}
