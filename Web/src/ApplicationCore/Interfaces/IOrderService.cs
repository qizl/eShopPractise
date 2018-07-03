using System.Threading.Tasks;
using EnjoyCodes.eShopOnWeb.ApplicationCore.Entities.OrderAggregate;

namespace EnjoyCodes.eShopOnWeb.ApplicationCore.Interfaces
{
    public interface IOrderService
    {
        Task CreateOrderAsync(int basketId, Address shippingAddress);
    }
}
