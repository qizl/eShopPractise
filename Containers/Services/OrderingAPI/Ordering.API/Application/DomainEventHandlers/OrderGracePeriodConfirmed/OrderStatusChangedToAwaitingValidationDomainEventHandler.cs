﻿namespace EnjoyCodes.eShopOnContainers.Services.Ordering.API.Application.DomainEventHandlers.OrderGracePeriodConfirmed
{
    using Domain.Events;
    using global::EnjoyCodes.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.BuyerAggregate;
    using global::EnjoyCodes.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.OrderAggregate;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using Ordering.API.Application.IntegrationEvents;
    using Ordering.API.Application.IntegrationEvents.Events;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    namespace EnjoyCodes.eShopOnContainers.Services.Ordering.API.Application.DomainEventHandlers.OrderCancelled
    {
        public class OrderStatusChangedToAwaitingValidationDomainEventHandler
                     : INotificationHandler<OrderStatusChangedToAwaitingValidationDomainEvent>
        {
            private readonly IOrderRepository _orderRepository;
            private readonly ILoggerFactory _logger;
            private readonly IBuyerRepository _buyerRepository;
            private readonly IOrderingIntegrationEventService _orderingIntegrationEventService;

            public OrderStatusChangedToAwaitingValidationDomainEventHandler(
                IOrderRepository orderRepository, ILoggerFactory logger,
                IBuyerRepository buyerRepository,
                IOrderingIntegrationEventService orderingIntegrationEventService)
            {
                _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
                _logger = logger ?? throw new ArgumentNullException(nameof(logger));
                _buyerRepository = buyerRepository;
                _orderingIntegrationEventService = orderingIntegrationEventService;
            }

            public async Task Handle(OrderStatusChangedToAwaitingValidationDomainEvent orderStatusChangedToAwaitingValidationDomainEvent, CancellationToken cancellationToken)
            {
                _logger.CreateLogger(nameof(OrderStatusChangedToAwaitingValidationDomainEvent))
                      .LogTrace($"Order with Id: {orderStatusChangedToAwaitingValidationDomainEvent.OrderId} has been successfully updated with " +
                                $"a status order id: {OrderStatus.AwaitingValidation.Id}");

                var order = await _orderRepository.GetAsync(orderStatusChangedToAwaitingValidationDomainEvent.OrderId);

                var buyer = await _buyerRepository.FindByIdAsync(order.GetBuyerId.Value.ToString());

                var orderStockList = orderStatusChangedToAwaitingValidationDomainEvent.OrderItems
                    .Select(orderItem => new OrderStockItem(orderItem.ProductId, orderItem.GetUnits()));

                var orderStatusChangedToAwaitingValidationIntegrationEvent = new OrderStatusChangedToAwaitingValidationIntegrationEvent(
                    order.Id, order.OrderStatus.Name, buyer.Name, orderStockList);
                await _orderingIntegrationEventService.PublishThroughEventBusAsync(orderStatusChangedToAwaitingValidationIntegrationEvent);
            }
        }
    }
}