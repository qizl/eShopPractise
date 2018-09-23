using EnjoyCodes.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace EnjoyCodes.eShopOnContainers.Services.Ordering.API.Application.IntegrationEvents.Events
{
    public class OrderPaymentFailedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }

        public OrderPaymentFailedIntegrationEvent(int orderId) => OrderId = orderId;
    }
}