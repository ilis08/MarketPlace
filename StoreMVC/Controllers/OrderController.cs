using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoreMVC.Models.Order;
using StoreMVC.Models.Order.OrderGetModels;
using StoreMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace StoreMVC.Controllers;

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

        var response = await client.PostAsJsonAsync("Order/Save", order);

        if (response.IsSuccessStatusCode)
        {
            var orderToReturn = JsonConvert.DeserializeObject<OrderGetById>(await response.Content.ReadAsStringAsync());
            Cart.CleanCart();
            return RedirectToAction("OrderCompleted", "Order", orderToReturn);
        }

        return View("Checkout", order);
    }

    [HttpGet]
    public IActionResult OrderCompleted(OrderGetById order)
    {
        return View(order);
    }

}
