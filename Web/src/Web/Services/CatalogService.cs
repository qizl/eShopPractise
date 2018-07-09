﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnjoyCodes.eShopOnWeb.ApplicationCore.Entities;
using EnjoyCodes.eShopOnWeb.ApplicationCore.Interfaces;
using EnjoyCodes.eShopOnWeb.ApplicationCore.Specifications;
using EnjoyCodes.eShopOnWeb.Web.Interfaces;
using EnjoyCodes.eShopOnWeb.Web.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace EnjoyCodes.eShopOnWeb.Web.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly ILogger<CatalogService> _logger;
        private readonly IRepository<CatalogItem> _itemRepository;
        private readonly IAsyncRepository<CatalogBrand> _brandRepository;
        private readonly IAsyncRepository<CatalogType> _typeRepository;
        private readonly IUriComposer _uriComposer;

        public CatalogService(ILoggerFactory loggerFactory, IRepository<CatalogItem> itemRepository, IAsyncRepository<CatalogBrand> brandRepository, IAsyncRepository<CatalogType> typeRepository, IUriComposer uriComposer)
        {
            this._logger = loggerFactory.CreateLogger<CatalogService>();
            this._itemRepository = itemRepository;
            this._brandRepository = brandRepository;
            this._typeRepository = typeRepository;
            this._uriComposer = uriComposer;
        }

        public async Task<CatalogIndexViewModel> GetCatalogItems(int pageIndex, int itemsPage, int? brandId, int? typeId)
        {
            this._logger.LogInformation("GetCatalogItems called.");

            var filterSpecification = new CatalogFilterSpecification(brandId, typeId);
            var root = this._itemRepository.List(filterSpecification);

            var totalItems = root.Count();

            var itemsOnPage = root.Skip(itemsPage * pageIndex).Take(itemsPage).ToList();

            itemsOnPage.ForEach(x =>
            {
                x.PictureUri = _uriComposer.ComposePicUri(x.PictureUri);
            });

            var vm = new CatalogIndexViewModel()
            {
                CatalogItems = itemsOnPage.Select(i => new CatalogItemViewModel()
                {
                    Id = i.Id,
                    Name = i.Name,
                    PictureUri = i.PictureUri,
                    Price = i.Price
                }),
                Brands = await GetBrands(),
                Types = await GetTypes(),
                BrandFilterApplied = brandId ?? 0,
                TypesFilterApplied = typeId ?? 0,
                PaginationInfo = new PaginationInfoViewModel()
                {
                    ActualPage = pageIndex,
                    ItemsPerPage = itemsOnPage.Count,
                    TotalItems = totalItems,
                    TotalPages = int.Parse(Math.Ceiling(((decimal)totalItems / itemsPage)).ToString())
                }
            };

            vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
            vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

            return vm;
        }

        public async Task<IEnumerable<SelectListItem>> GetBrands()
        {
            this._logger.LogInformation("GetBrands called.");
            var brands = await this._brandRepository.ListAllAsync();

            var items = new List<SelectListItem>
            {
                new SelectListItem() { Value = null, Text = "All", Selected = true }
            };
            foreach (CatalogBrand brand in brands)
                items.Add(new SelectListItem() { Value = brand.Id.ToString(), Text = brand.Brand });

            return items;
        }

        public async Task<IEnumerable<SelectListItem>> GetTypes()
        {
            this._logger.LogInformation("GetTypes called.");
            var types = await this._typeRepository.ListAllAsync();
            var items = new List<SelectListItem>
            {
                new SelectListItem() { Value = null, Text = "All", Selected = true }
            };
            foreach (CatalogType type in types)
                items.Add(new SelectListItem() { Value = type.Id.ToString(), Text = type.Type });

            return items;
        }
    }
}
