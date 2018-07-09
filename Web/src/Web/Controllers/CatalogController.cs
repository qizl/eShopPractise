using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EnjoyCodes.eShopOnWeb.Web.Models;

namespace EnjoyCodes.eShopOnWeb.Web.Controllers
{
    [Route("")]
    public class CatalogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
