﻿using EnjoyCodes.eShopOnContainers.WebMVC.Infrastructure;
using EnjoyCodes.eShopOnContainers.WebMVC.Models;
using EnjoyCodes.eShopOnContainers.WebMVC.ViewModels;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EnjoyCodes.eShopOnContainers.WebMVC.Services
{
    public class BasketService : IBasketService
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly HttpClient _apiClient;
        private readonly string _basketByPassUrl;
        private readonly string _purchaseUrl;

        public BasketService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            _apiClient = httpClient;
            _settings = settings;

            _basketByPassUrl = $"{_settings.Value.PurchaseUrl}/api/v1/b/basket";
            _purchaseUrl = $"{_settings.Value.PurchaseUrl}/api/v1/b/basket";
        }

        public async Task<Basket> GetBasket(ApplicationUser user)
        {
            var uri = API.Basket.GetBasket(_basketByPassUrl, user.Id);

            var responseString = await _apiClient.GetStringAsync(uri);

            return string.IsNullOrEmpty(responseString) ?
                new Basket() { BuyerId = user.Id } :
                JsonConvert.DeserializeObject<Basket>(responseString);
        }

        public async Task<Basket> UpdateBasket(Basket basket)
        {
            var uri = API.Basket.UpdateBasket(_basketByPassUrl);

            var basketContent = new StringContent(JsonConvert.SerializeObject(basket), System.Text.Encoding.UTF8, "application/json");

            var response = await _apiClient.PostAsync(uri, basketContent);

            response.EnsureSuccessStatusCode();

            return basket;
        }

        public async Task Checkout(BasketDTO basket)
        {
            var uri = API.Basket.CheckoutBasket(_basketByPassUrl);
            var basketContent = new StringContent(JsonConvert.SerializeObject(basket), System.Text.Encoding.UTF8, "application/json");

            var response = await _apiClient.PostAsync(uri, basketContent);

            response.EnsureSuccessStatusCode();
        }

        public async Task<Basket> SetQuantities(ApplicationUser user, Dictionary<string, int> quantities)
        {
            var uri = API.Purchase.UpdateBasketItem(_purchaseUrl);

            var basketUpdate = new
            {
                BuyerId = user.Id,
                Updates = quantities.Select(kvp => new
                {
                    BasketItemId = kvp.Key,
                    NewQty = kvp.Value
                })
            };

            var basketContent = new StringContent(JsonConvert.SerializeObject(basketUpdate), System.Text.Encoding.UTF8, "application/json");

            var response = await _apiClient.PutAsync(uri, basketContent);

            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Basket>(jsonResponse);
        }

        public async Task<Order> GetOrderDraft(string basketId)
        {
            //var uri = API.Purchase.GetOrderDraft(_purchaseUrl, basketId);

            //var responseString = await _apiClient.GetStringAsync(uri);

            //var response = JsonConvert.DeserializeObject<Order>(responseString);

            //return response;

            var uri = API.Basket.GetBasket(_basketByPassUrl, basketId);

            var responseString = await _apiClient.GetStringAsync(uri);

            var basket = string.IsNullOrEmpty(responseString) ?
                new Basket() { BuyerId = basketId } :
                JsonConvert.DeserializeObject<Basket>(responseString);

            var order = new Order();
            order.Total = basket.Items.Sum(m => m.UnitPrice * m.Quantity);
            order.OrderItems.AddRange(from i in basket.Items
                                      select new OrderItem()
                                      {
                                          ProductId = Convert.ToInt32(i.ProductId),
                                          ProductName = i.ProductName,
                                          UnitPrice = i.UnitPrice,
                                          Units = i.Quantity,
                                          PictureUrl = i.PictureUrl
                                      });
            return order;
        }

        public async Task AddItemToBasket(ApplicationUser user, CatalogItem product)
        {
            var uri = API.Purchase.AddItemToBasket(_purchaseUrl);

            var newItem = new
            {
                BuyerId = user.Id,
                Items = new List<object>
                {
                    new {
                        ProductId   = product.Id,
                        ProductName = product.Name,
                        UnitPrice   = product.Price,
                        PictureUrl  = product.PictureUri,
                        Quantity    = 1
                    }
                }
                //CatalogItemId = productId,
                //BasketId = user.Id,
                //Quantity = 1
            };

            var basketContent = new StringContent(JsonConvert.SerializeObject(newItem), System.Text.Encoding.UTF8, "application/json");

            var response = await _apiClient.PostAsync(uri, basketContent);
        }
    }
}