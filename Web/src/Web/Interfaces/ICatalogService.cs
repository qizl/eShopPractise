using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnjoyCodes.eShopOnWeb.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EnjoyCodes.eShopOnWeb.Web.Interfaces
{
    public interface ICatalogService
    {
        Task<CatalogIndexViewModel> GetCatalogItems(int pageIndex, int itemsPage, int? brandId, int? typeId);
        Task<IEnumerable<SelectListItem>> GetBrands();
        Task<IEnumerable<SelectListItem>> GetTypes();
    }
}
