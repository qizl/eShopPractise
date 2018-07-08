using System;
using System.Collections.Generic;
using EnjoyCodes.eShopOnWeb.ApplicationCore.Entities.OrderAggregate;

namespace EnjoyCodes.eShopOnWeb.Web.ViewModels
{
    public class OrderViewModel
    {
        public int OrderNumber { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public decimal Total { get; set; }
        public string Status { get; set; }

        public Address ShippingAddress { get; set; }

        public List<OrderItemViewModel> OrderItems { get; set; } = new List<OrderItemViewModel>();
    }
}
