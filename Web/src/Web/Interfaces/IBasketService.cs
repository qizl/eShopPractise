using System.Threading.Tasks;
using EnjoyCodes.eShopOnWeb.Web.ViewModels;

namespace EnjoyCodes.eShopOnWeb.Web.Interfaces
{
    public interface IBasketViewModelService
    {
        Task<BasketViewModel> GetOrCreateBasketForUser(string userName);
    }
}
