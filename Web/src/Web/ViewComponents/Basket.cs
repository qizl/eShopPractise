using System.Linq;
using System.Threading.Tasks;
using EnjoyCodes.eShopOnWeb.Infrastructure.Identity;
using EnjoyCodes.eShopOnWeb.Web.Interfaces;
using EnjoyCodes.eShopOnWeb.Web.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EnjoyCodes.eShopOnWeb.Web.ViewComponents
{
    public class Basket : ViewComponent
    {
        private readonly IBasketViewModelService _basketService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public Basket(IBasketViewModelService basketService, SignInManager<ApplicationUser> signInManager)
        {
            this._basketService = basketService;
            this._signInManager = signInManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(string userName)
        {
            var vm = new BasketComponentViewModel();
            vm.ItemsCount = (await GetBasketViewModelAsync()).Items.Sum(i => i.Quantity);
            return View(vm);
        }

        private async Task<BasketViewModel> GetBasketViewModelAsync()
        {
            if (this._signInManager.IsSignedIn(HttpContext.User))
                return await this._basketService.GetOrCreateBasketForUser(User.Identity.Name);

            var anonymousId = GetBasketIdFromCookie();
            if (anonymousId == null) return new BasketViewModel();
            return await this._basketService.GetOrCreateBasketForUser(anonymousId);
        }

        private string GetBasketIdFromCookie()
        {
            if (Request.Cookies.ContainsKey(Constants.BASKET_COOKIENAME))
                return Request.Cookies[Constants.BASKET_COOKIENAME];
            return null;
        }
    }
}
