using EnjoyCodes.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace EnjoyCodes.eShopOnContainers.Services.Ordering.API.Application.IntegrationEvents.Events
{
    public class GracePeriodConfirmedIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }

        public GracePeriodConfirmedIntegrationEvent(int orderId) =>
            OrderId = orderId;
    }
}
