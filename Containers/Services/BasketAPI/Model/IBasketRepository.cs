using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnjoyCodes.eShopOnContainers.Services.BasketAPI.Model
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(string customerId);
        IEnumerable<string> GetUsers();
        Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket);
        Task<bool> DeleteBasketAsync(string id);
    }
}
