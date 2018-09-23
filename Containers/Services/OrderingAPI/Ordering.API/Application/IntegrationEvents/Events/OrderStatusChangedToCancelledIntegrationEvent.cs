using EnjoyCodes.eShopOnContainers.BuildingBlocks.EventBus.Events;

namespace EnjoyCodes.eShopOnContainers.Services.Ordering.API.Application.IntegrationEvents.Events
{
    public class OrderStatusChangedToCancelledIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }
        public string OrderStatus { get; }
        public string BuyerName { get; }

        public OrderStatusChangedToCancelledIntegrationEvent(int orderId, string orderStatus, string buyerName)
        {
            OrderId = orderId;
            OrderStatus = orderStatus;
            BuyerName = buyerName;
        }
    }
}
