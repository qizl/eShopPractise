using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EnjoyCodes.eShopOnWeb.Web.Interfaces;
using EnjoyCodes.eShopOnWeb.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;

namespace EnjoyCodes.eShopOnWeb.Web.Services
{
    public class CachedCatalogService : ICatalogService
    {
        private readonly IMemoryCache cache;
        private readonly CatalogService _catalogService;
        private static readonly string brandsKey = "brands";
        private static readonly string typesKey = "types";
        private static readonly string itemsKeyTemplate = "items-{0}-{1}-{2}-{3}";
        private static readonly TimeSpan defaultCacheDuration = TimeSpan.FromSeconds(30);

        public CachedCatalogService(IMemoryCache cache, CatalogService catalogService)
        {
            this.cache = cache;
            this._catalogService = catalogService;
        }

        public async Task<IEnumerable<SelectListItem>> GetBrands()
        {
            return await this.cache.GetOrCreateAsync(brandsKey, async entry =>
                    {
                        entry.SlidingExpiration = defaultCacheDuration;
                        return await this._catalogService.GetBrands();
                    });
        }

        public async Task<CatalogIndexViewModel> GetCatalogItems(int pageIndex, int itemsPage, int? brandId, int? typeId)
        {
            string cacheKey = String.Format(itemsKeyTemplate, pageIndex, itemsPage, brandId, typeId);
            return await cache.GetOrCreateAsync(cacheKey, async entry =>
            {
                entry.SlidingExpiration = defaultCacheDuration;
                return await this._catalogService.GetCatalogItems(pageIndex, itemsPage, brandId, typeId);
            });
        }

        public async Task<IEnumerable<SelectListItem>> GetTypes()
        {
            return await cache.GetOrCreateAsync(typesKey, async entry =>
            {
                entry.SlidingExpiration = defaultCacheDuration;
                return await _catalogService.GetTypes();
            });
        }
    }
}
