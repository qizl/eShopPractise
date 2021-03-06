﻿using EnjoyCodes.eShopOnContainers.BuildingBlocks.EventBus.Abstractions;
using EnjoyCodes.eShopOnContainers.Services.BasketAPI.IntegrationEvents.Events;
using EnjoyCodes.eShopOnContainers.Services.BasketAPI.Model;
using EnjoyCodes.eShopOnContainers.Services.BasketAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EnjoyCodes.eShopOnContainers.Services.BasketAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [Authorize]
    public class BasketController : Controller
    {
        private readonly IBasketRepository _repository;
        private readonly IIdentityService _identitySvc;
        private readonly IEventBus _eventBus;

        public BasketController(IBasketRepository repository,
            IIdentityService identityService,
            IEventBus eventBus)
        {
            _repository = repository;
            _identitySvc = identityService;
            _eventBus = eventBus;
        }

        // GET /id
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CustomerBasket), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(string id)
        {
            var basket = await _repository.GetBasketAsync(id);
            if (basket == null)
            {
                return Ok(new CustomerBasket(id) { });
            }

            return Ok(basket);
        }

        // POST /value
        [HttpPost]
        [ProducesResponseType(typeof(CustomerBasket), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post([FromBody]CustomerBasket value)
        {
            var basket = await this._repository.GetBasketAsync(value.BuyerId);
            if (basket != null)
            {
                foreach (var item in value.Items)
                {
                    var oldItem = basket.Items.FirstOrDefault(m => m.ProductId == item.ProductId);
                    if (oldItem != null)
                        oldItem.Quantity++;
                    else
                        basket.Items.Add(item);
                }
            }
            else
            {
                basket = value;
            }

            var basket1 = await _repository.UpdateBasketAsync(basket);

            return Ok(basket1);
        }

        // PUT /value
        [HttpPut]
        [ProducesResponseType(typeof(CustomerBasket), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put([FromBody]UpdateBasket value)
        {
            var basket = await this._repository.GetBasketAsync(value.BuyerId);
            basket.Items.ForEach(m =>
            {
                m.Quantity = value.Updates.First(i => i.BasketItemId == m.ProductId).NewQty;
            });

            var basket1 = await _repository.UpdateBasketAsync(basket);

            return Ok(basket);
        }

        [Route("checkout")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout([FromBody]BasketCheckout basketCheckout, [FromHeader(Name = "x-requestid")] string requestId)
        {
            var userId = _identitySvc.GetUserIdentity();

            basketCheckout.RequestId = (Guid.TryParse(requestId, out Guid guid) && guid != Guid.Empty) ?
                guid : basketCheckout.RequestId;

            var basket = await _repository.GetBasketAsync(userId);

            if (basket == null)
            {
                return BadRequest();
            }

            var userName = User.FindFirst(x => x.Type == "unique_name").Value;

            var eventMessage = new UserCheckoutAcceptedIntegrationEvent(userId, userName, basketCheckout.City, basketCheckout.Street,
                basketCheckout.State, basketCheckout.Country, basketCheckout.ZipCode, basketCheckout.CardNumber, basketCheckout.CardHolderName,
                basketCheckout.CardExpiration, basketCheckout.CardSecurityNumber, basketCheckout.CardTypeId, basketCheckout.Buyer, basketCheckout.RequestId, basket);

            // Once basket is checkout, sends an integration event to
            // ordering.api to convert basket to order and proceeds with
            // order creation process
            _eventBus.Publish(eventMessage);

            return Accepted();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _repository.DeleteBasketAsync(id);
        }
    }
}
