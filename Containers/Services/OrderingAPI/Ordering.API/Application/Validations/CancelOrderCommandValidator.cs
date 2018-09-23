using EnjoyCodes.eShopOnContainers.Services.Ordering.API.Application.Commands;
using FluentValidation;

namespace EnjoyCodes.eShopOnContainers.Services.Ordering.API.Application.Validations
{
    public class CancelOrderCommandValidator : AbstractValidator<CancelOrderCommand>
    {
        public CancelOrderCommandValidator()
        {
            RuleFor(order => order.OrderNumber).NotEmpty().WithMessage("No orderId found");
        }
    }
}
