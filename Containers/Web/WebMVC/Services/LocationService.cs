﻿using EnjoyCodes.eShopOnContainers.WebMVC;
using EnjoyCodes.eShopOnContainers.WebMVC.Infrastructure;
using EnjoyCodes.eShopOnContainers.WebMVC.Models;
using EnjoyCodes.eShopOnContainers.WebMVC.Services;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebMVC.Services
{
    public class LocationService : ILocationService
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly HttpClient _httpClient;
        private readonly ILogger<CampaignService> _logger;
        private readonly string _remoteServiceBaseUrl;

        public LocationService(HttpClient httpClient, IOptions<AppSettings> settings, ILogger<CampaignService> logger)
        {
            _httpClient = httpClient;
            _settings = settings;
            _logger = logger;

            _remoteServiceBaseUrl = $"{_settings.Value.PurchaseUrl}/api/v1/l/locations/";
        }

        public async Task CreateOrUpdateUserLocation(LocationDTO location)
        {
            var uri = API.Locations.CreateOrUpdateUserLocation(_remoteServiceBaseUrl);
            var locationContent = new StringContent(JsonConvert.SerializeObject(location), System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(uri, locationContent);
            response.EnsureSuccessStatusCode();
        }
    }
}
