﻿namespace EnjoyCodes.eShopOnContainers.Services.CatalogAPI.IntegrationEvents.Events
{
    using BuildingBlocks.EventBus.Events;
    using System.Collections.Generic;

    public class OrderStatusChangedToPaidIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; }
        public IEnumerable<OrderStockItem> OrderStockItems { get; }

        public OrderStatusChangedToPaidIntegrationEvent(int orderId,
            IEnumerable<OrderStockItem> orderStockItems)
        {
            OrderId = orderId;
            OrderStockItems = orderStockItems;
        }
    }
}