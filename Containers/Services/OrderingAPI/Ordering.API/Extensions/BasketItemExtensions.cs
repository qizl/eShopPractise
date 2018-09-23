using EnjoyCodes.eShopOnContainers.Services.Ordering.API.Application.Models;
using System.Collections.Generic;
using static EnjoyCodes.eShopOnContainers.Services.Ordering.API.Application.Commands.CreateOrderCommand;

namespace EnjoyCodes.eShopOnContainers.Services.Ordering.API.Extensions
{
    public static class BasketItemExtensions
    {
        public static IEnumerable<OrderItemDTO> ToOrderItemsDTO(this IEnumerable<BasketItem> basketItems)
        {
            foreach (var item in basketItems)
            {
                yield return item.ToOrderItemDTO();
            }
        }

        public static OrderItemDTO ToOrderItemDTO(this BasketItem item)
        {
            return new OrderItemDTO()
            {
                ProductId = int.TryParse(item.ProductId, out int id) ? id : -1,
                ProductName = item.ProductName,
                PictureUrl = item.PictureUrl,
                UnitPrice = item.UnitPrice,
                Units = item.Quantity
            };
        }
    }
}
