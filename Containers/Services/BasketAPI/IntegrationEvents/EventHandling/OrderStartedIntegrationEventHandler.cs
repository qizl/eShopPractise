using EnjoyCodes.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using EnjoyCodes.eShopOnContainers.Services.BasketAPI.IntegrationEvents.Events;
using EnjoyCodes.eShopOnContainers.Services.BasketAPI.Model;
using System;
using System.Threading.Tasks;

namespace EnjoyCodes.eShopOnContainers.Services.BasketAPI.IntegrationEvents.EventHandling
{
    public class OrderStartedIntegrationEventHandler : IIntegrationEventHandler<OrderStartedIntegrationEvent>
    {
        private readonly IBasketRepository _repository;

        public OrderStartedIntegrationEventHandler(IBasketRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task Handle(OrderStartedIntegrationEvent @event)
        {
            await _repository.DeleteBasketAsync(@event.UserId.ToString());
        }
    }
}
