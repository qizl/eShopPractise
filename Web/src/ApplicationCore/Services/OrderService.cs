using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.GuardClauses;
using EnjoyCodes.eShopOnWeb.ApplicationCore.Entities;
using EnjoyCodes.eShopOnWeb.ApplicationCore.Entities.BasketAggregate;
using EnjoyCodes.eShopOnWeb.ApplicationCore.Entities.OrderAggregate;
using EnjoyCodes.eShopOnWeb.ApplicationCore.Interfaces;

namespace EnjoyCodes.eShopOnWeb.ApplicationCore.Services
{
    public class OrderService : IOrderService
    {
        private readonly IAsyncRepository<Order> _orderRepository;
        private readonly IAsyncRepository<Basket> _basketRepository;
        private readonly IAsyncRepository<CatalogItem> _itemRepository;

        public OrderService(IAsyncRepository<Basket> basketRepository,
            IAsyncRepository<CatalogItem> itemRepository,
            IAsyncRepository<Order> orderRepository)
        {
            this._orderRepository = orderRepository;
            this._basketRepository = basketRepository;
            this._itemRepository = itemRepository;
        }

        public async Task CreateOrderAsync(int basketId, Address shippingAddress)
        {
            var basket = await _basketRepository.GetByIdAsync(basketId);
            Guard.Against.NullBasket(basketId, basket);
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var catalogItem = await _itemRepository.GetByIdAsync(item.CatalogItemId);
                var itemOrdered = new CatalogItemOrdered(catalogItem.Id, catalogItem.Name, catalogItem.PictureUri);
                items.Add(new OrderItem(itemOrdered, item.UnitPrice, item.Quantity));
            }
            var order = new Order(basket.BuyerId, shippingAddress, items);

            await _orderRepository.AddAsync(order);
        }
    }
}
