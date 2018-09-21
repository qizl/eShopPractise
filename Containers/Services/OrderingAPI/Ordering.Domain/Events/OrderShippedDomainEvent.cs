using EnjoyCodes.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.OrderAggregate;
using MediatR;

namespace EnjoyCodes.eShopOnContainers.Services.Ordering.Domain.Events
{
    public class OrderShippedDomainEvent : INotification
    {
        public Order Order { get; }

        public OrderShippedDomainEvent(Order order)
        {
            Order = order;
        }
    }
}
