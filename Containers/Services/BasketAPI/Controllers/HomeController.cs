using Microsoft.AspNetCore.Mvc;

namespace EnjoyCodes.eShopOnContainers.Services.BasketAPI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => new RedirectResult("~/swagger");
    }
}
