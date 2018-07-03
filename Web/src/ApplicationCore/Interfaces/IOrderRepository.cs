using System.Threading.Tasks;
using EnjoyCodes.eShopOnWeb.ApplicationCore.Entities.OrderAggregate;

namespace EnjoyCodes.eShopOnWeb.ApplicationCore.Interfaces
{
    public interface IOrderRepository : IRepository<Order>, IAsyncRepository<Order>
    {
        Order GetByIdWithItems(int id);
        Task<Order> GetByIdWithItemsAsync(int id);
    }
}
