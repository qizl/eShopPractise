using System.Threading.Tasks;
using EnjoyCodes.eShopOnWeb.Web.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Api
{
    public class CatalogController : BaseApiController
    {
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService) => this._catalogService = catalogService;

        [HttpGet]
        public async Task<IActionResult> List(int? brandFilterApplied, int? typesFilterApplied, int? page)
        {
            var itemsPage = 10;
            var catalogModel = await this._catalogService.GetCatalogItems(page ?? 0, itemsPage, brandFilterApplied, typesFilterApplied);
            return Ok(catalogModel);
        }
    }
}