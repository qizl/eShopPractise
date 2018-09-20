using Microsoft.AspNetCore.Mvc;

namespace EnjoyCodes.eShopOnContainers.Services.LocationsAPI.Controllers
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
