using System.Linq;
using System.Threading.Tasks;
using EnjoyCodes.eShopOnWeb.ApplicationCore.Entities.OrderAggregate;
using EnjoyCodes.eShopOnWeb.ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EnjoyCodes.eShopOnWeb.Infrastructure.Data
{
    public class OrderRepository : EfRepository<Order>, IOrderRepository
    {
        public OrderRepository(CatalogContext dbContext) : base(dbContext) { }

        public Order GetByIdWithItems(int id) =>
            this._dbContext.Orders
            .Include(o => o.OrderItems)
            .Include($"{nameof(Order.OrderItems)}.{nameof(OrderItem.ItemOrdered)}")
            .FirstOrDefault();

        public Task<Order> GetByIdWithItemsAsync(int id) =>
            this._dbContext.Orders
            .Include(o => o.OrderItems)
            .Include($"{nameof(Order.OrderItems)}.{nameof(OrderItem.ItemOrdered)}")
            .FirstOrDefaultAsync();
    }
}
