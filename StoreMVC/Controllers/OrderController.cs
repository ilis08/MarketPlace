using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoreMVC.Models.Order;
using StoreMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
        public IActionResult Checkout()
        {
            if (Cart.Lines.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            Order.OrderDetailProducts = Cart.Lines;

            return View(Order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(OrderVM order)
        {
            order.OrderDetailProducts = Cart.Lines;

            var client = _clientFactory.CreateClient("myapi");

            var content = JsonConvert.SerializeObject(order);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            var response = await client.PostAsync("Order/Save", byteContent);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

    }
}
