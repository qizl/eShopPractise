﻿using EnjoyCodes.eShopOnContainers.Services.Ordering.API.Application.Models;
using MediatR;
using System.Collections.Generic;

namespace EnjoyCodes.eShopOnContainers.Services.Ordering.API.Application.Commands
{
    public class CreateOrderDraftCommand :  IRequest<OrderDraftDTO>
    {
       
        public string BuyerId { get; private set; }

        public IEnumerable<BasketItem> Items { get; private set; }

        public CreateOrderDraftCommand(string buyerId, IEnumerable<BasketItem> items)
        {
            BuyerId = buyerId;
            Items = items;
        }
    }

}
