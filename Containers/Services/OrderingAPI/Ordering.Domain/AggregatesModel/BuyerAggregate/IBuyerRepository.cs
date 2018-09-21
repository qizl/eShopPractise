using EnjoyCodes.eShopOnContainers.Services.Ordering.Domain.SeedWork;
using System.Threading.Tasks;

namespace EnjoyCodes.eShopOnContainers.Services.Ordering.Domain.AggregatesModel.BuyerAggregate
{
    public interface IBuyerRepository : IRepository<Buyer>
    {
        Buyer Add(Buyer buyer);
        Buyer Update(Buyer buyer);
        Task<Buyer> FindAsync(string BuyerIdentityGuid);
        Task<Buyer> FindByIdAsync(string id);
    }
}
