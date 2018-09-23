using EnjoyCodes.eShopOnContainers.WebMVC.Models;
using EnjoyCodes.eShopOnContainers.WebMVC.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnjoyCodes.eShopOnContainers.WebMVC.Services
{
    public interface IBasketService
    {
        Task<Basket> GetBasket(ApplicationUser user);
        Task AddItemToBasket(ApplicationUser user, CatalogItem product);
        Task<Basket> UpdateBasket(Basket basket);
        Task Checkout(BasketDTO basket);
        Task<Basket> SetQuantities(ApplicationUser user, Dictionary<string, int> quantities);
        Task<Order> GetOrderDraft(string basketId);
    }
}
