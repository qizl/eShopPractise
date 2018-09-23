using Microsoft.AspNetCore.Mvc;

namespace EnjoyCodes.eShopOnContainers.Services.Ordering.API.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}
