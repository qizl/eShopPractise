using EnjoyCodes.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.OrderAggregate;
using MediatR;

namespace EnjoyCodes.eShopOnContainers.Services.Ordering.Domain.Events
{
    public class OrderCancelledDomainEvent : INotification
    {
        public Order Order { get; }

        public OrderCancelledDomainEvent(Order order)
        {
            Order = order;
        }
    }
}
