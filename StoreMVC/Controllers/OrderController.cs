using Microsoft.AspNetCore.Mvc;
using StoreMVC.Models.Order;
using StoreMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StoreMVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        public OrderVM Order { get; set; } = new OrderVM();
        public Cart Cart { get; set; }

        public OrderController(IHttpClientFactory clientFactory, Cart cart)
        {
            _clientFactory = clientFactory;
            Cart = cart;
        }

        [HttpGet]
        public async Task<IActionResult> Checkout()
        {
            Order.OrderDetailProducts = Cart.Lines;

            return View(Order);
        }
    }
}
