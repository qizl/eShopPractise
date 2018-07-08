using System.Threading.Tasks;
using EnjoyCodes.eShopOnWeb.Web.ViewModels;

namespace EnjoyCodes.eShopOnWeb.Web.Interfaces
{
    public interface IBasketService
    {
        Task<BasketViewModel> GetOrCreateBasketForUser(string userName);
    }
}
