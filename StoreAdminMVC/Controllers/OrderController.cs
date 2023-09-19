using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoreAdminMVC.ViewModels;
using StoreAdminMVC.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace StoreAdminMVC.Controllers;

public class OrderController : Controller
{
    private readonly IHttpClientFactory _clientFactory;

    public IEnumerable<OrderGetVM> Orders { get; private set; }

    public OrderGetByIdVM Order { get; set; }

    public bool GetPullRequestsError { get; private set; }


    public OrderController(IHttpClientFactory clientFactory, HttpClient httpClient)
    {
        _clientFactory = clientFactory;
    }

    public async Task<ActionResult> Index()
    {
        var client = _clientFactory.CreateClient("myapi");

        var response = await client.GetAsync("Order/Get/");

        if (response.IsSuccessStatusCode)
        {
            Orders = JsonConvert.DeserializeObject<List<OrderGetVM>>(await response.Content.ReadAsStringAsync());
        }
        else
        {
            GetPullRequestsError = true;
            Orders = Array.Empty<OrderGetVM>();
        }

        return View(Orders);
    }

    public async Task<IActionResult> Details(int id)
    {
        var client = _clientFactory.CreateClient("myapi");

        var response = await client.GetAsync($"Order/GetById/{id}");

        if (response.IsSuccessStatusCode)
        {
            Order = JsonConvert.DeserializeObject<OrderGetByIdVM>(await response.Content.ReadAsStringAsync());
        }
        else
        {
            GetPullRequestsError = true;
            Order = null;
        }

        return View(Order);
    }

    [HttpGet]
    public async Task<ActionResult> CompleteOrder(int id)
    {
        var client = _clientFactory.CreateClient("myapi");

        var response = await client.PutAsync($"Order/CompleteOrder/{id}", null);

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("Details", new { Id = id });
        }
        else
        {
            return NotFound();
        }
    }
}
