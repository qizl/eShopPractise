namespace EnjoyCodes.eShopOnContainers.Services.Ordering.API.Application.IntegrationEvents.Events
{
    using EnjoyCodes.eShopOnContainers.BuildingBlocks.EventBus.Events;

    public class OrderPaymentSuccededIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }

        public OrderPaymentSuccededIntegrationEvent(int orderId) => OrderId = orderId;
    }
}