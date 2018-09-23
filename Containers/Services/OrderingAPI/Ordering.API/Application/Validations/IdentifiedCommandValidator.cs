using EnjoyCodes.eShopOnContainers.Services.Ordering.API.Application.Commands;
using FluentValidation;

namespace EnjoyCodes.eShopOnContainers.Services.Ordering.API.Application.Validations
{
    public class IdentifiedCommandValidator : AbstractValidator<IdentifiedCommand<CreateOrderCommand,bool>>
    {
        public IdentifiedCommandValidator()
        {
            RuleFor(command => command.Id).NotEmpty();    
        }
    }
}
