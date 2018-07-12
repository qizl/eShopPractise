using EnjoyCodes.eShopOnWeb.ApplicationCore.Entities.OrderAggregate;
using EnjoyCodes.eShopOnWeb.ApplicationCore.Interfaces;
using EnjoyCodes.eShopOnWeb.Infrastructure.Identity;
using EnjoyCodes.eShopOnWeb.Web.Interfaces;
using EnjoyCodes.eShopOnWeb.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnjoyCodes.eShopOnWeb.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IUriComposer _uriComposer;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAppLogger<BasketController> _logger;
        private readonly IOrderService _orderService;
        private readonly IBasketViewModelService _basketViewModelService;

        public BasketController(IBasketService basketService, IBasketViewModelService basketViewModelService, IOrderService orderService, IUriComposer uriComposer, SignInManager<ApplicationUser> signInManager, IAppLogger<BasketController> logger)
        {
            this._basketService = basketService;
            this._uriComposer = uriComposer;
            this._signInManager = signInManager;
            this._logger = logger;
            this._orderService = orderService;
            this._basketViewModelService = basketViewModelService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var basketModel = await GetBasketViewModelAsync();

            return View(basketModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(Dictionary<string, int> items)
        {
            var basketViewModel = await GetBasketViewModelAsync();
            await this._basketService.SetQuantities(basketViewModel.Id, items);
            return View(await this.GetBasketViewModelAsync());
        }

        [HttpPost]
        public async Task<IActionResult> AddToBasket(CatalogItemViewModel productDetails)
        {
            if (productDetails?.Id == null)
                return RedirectToAction(nameof(CatalogController.Index), "Catalog");

            var basketViewModel = await this.GetBasketViewModelAsync();
            await this._basketService.AddItemToBasket(basketViewModel.Id, productDetails.Id, productDetails.Price, 1);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Checkout(Dictionary<string, int> items)
        {
            var basketViewModel = await this.GetBasketViewModelAsync();
            await this._basketService.SetQuantities(basketViewModel.Id, items);
            await this._orderService.CreateOrderAsync(basketViewModel.Id, new Address("123 Main St.", "Kent", "OH", "United States", "44240"));
            await this._basketService.DeleteBasketAsync(basketViewModel.Id);
            return View(nameof(Checkout));
        }

        private async Task<BasketViewModel> GetBasketViewModelAsync()
        {
            if (this._signInManager.IsSignedIn(HttpContext.User))
                return await this._basketViewModelService.GetOrCreateBasketForUser(User.Identity.Name);

            var anonymousId = GetOrSetBasketCookie();
            return await this._basketViewModelService.GetOrCreateBasketForUser(anonymousId);
        }

        private string GetOrSetBasketCookie()
        {
            if (Request.Cookies.ContainsKey(Constants.BASKET_COOKIENAME))
                return Request.Cookies[Constants.BASKET_COOKIENAME];

            var anonymousId = Guid.NewGuid().ToString();
            var cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Today.AddYears(10);
            Response.Cookies.Append(Constants.BASKET_COOKIENAME, anonymousId, cookieOptions);
            return anonymousId;
        }
    }
}