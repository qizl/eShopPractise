﻿using System.Linq;
using System.Threading.Tasks;
using EnjoyCodes.eShopOnWeb.ApplicationCore.Interfaces;
using EnjoyCodes.eShopOnWeb.ApplicationCore.Specifications;
using EnjoyCodes.eShopOnWeb.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnjoyCodes.eShopOnWeb.Web.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await this._orderRepository.ListAsync(new CustomerOrdersWithItemsSpecification(User.Identity.Name));

            var viewModel = orders.Select(o => new OrderViewModel()
            {
                OrderDate = o.OrderDate,
                OrderItems = o.OrderItems?.Select(oi => new OrderItemViewModel()
                {
                    Discount = 0,
                    PictureUrl = oi.ItemOrdered.PictureUri,
                    ProductId = oi.ItemOrdered.CatalogItemId,
                    ProductName = oi.ItemOrdered.ProductName,
                    UnitPrice = oi.UnitPrice,
                    Units = oi.Units
                }).ToList(),
                OrderNumber = o.Id,
                ShippingAddress = o.ShipToAddress,
                Status = "Pending",
                Total = o.Total()

            });
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int orderId)
        {
            var customerOrders = await this._orderRepository.ListAsync(new CustomerOrdersWithItemsSpecification(User.Identity.Name));
            var order = customerOrders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
                return BadRequest("No such order found for this user.");
            var viewModel = new OrderViewModel()
            {
                OrderDate = order.OrderDate,
                OrderItems = order.OrderItems.Select(oi => new OrderItemViewModel()
                {
                    Discount = 0,
                    PictureUrl = oi.ItemOrdered.PictureUri,
                    ProductId = oi.ItemOrdered.CatalogItemId,
                    ProductName = oi.ItemOrdered.ProductName,
                    UnitPrice = oi.UnitPrice,
                    Units = oi.Units
                }).ToList(),
                OrderNumber = order.Id,
                ShippingAddress = order.ShipToAddress,
                Status = "Pending",
                Total = order.Total()
            };
            return View(viewModel);
        }
    }
}