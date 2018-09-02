using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnjoyCodes.eShopOnContainers.Services.BasketAPI.Model
{
    public class RedisBasketRepository : IBasketRepository
    {
        private readonly ILogger<RedisBasketRepository> _logger;
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public RedisBasketRepository(ILoggerFactory loggerFactory, ConnectionMultiplexer redis)
        {
            this._logger = loggerFactory.CreateLogger<RedisBasketRepository>();
            this._redis = redis;
            this._database = redis.GetDatabase();
        }

        public async Task<bool> DeleteBasketAsync(string id) => await this._database.KeyDeleteAsync(id);

        public IEnumerable<string> GetUsers()
        {
            var server = GetServer();
            var data = server.Keys();
            return data?.Select(k => k.ToString());
        }

        public async Task<CustomerBasket> GetBasketAsync(string customerId)
        {
            var data = await this._database.StringGetAsync(customerId);
            if (data.IsNullOrEmpty)
                return null;

            return JsonConvert.DeserializeObject<CustomerBasket>(data);
        }

        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket basket)
        {
            var created = await this._database.StringSetAsync(basket.BuyerId, JsonConvert.SerializeObject(basket));
            if (!created)
            {
                this._logger.LogInformation("Problem occur persisting the item.");
                return null;
            }

            this._logger.LogInformation("Basket item persisted succesfully.");

            return await GetBasketAsync(basket.BuyerId);
        }

        private IServer GetServer()
        {
            var endpoint = this._redis.GetEndPoints();
            return this._redis.GetServer(endpoint.First());
        }
    }
}
