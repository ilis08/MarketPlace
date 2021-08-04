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

namespace StoreAdminMVC.Controllers
{
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
            var request = new HttpRequestMessage(HttpMethod.Get, "Order/Get/");

            var client = _clientFactory.CreateClient("myapi");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                Orders = JsonConvert.DeserializeObject<List<OrderGetVM>>(responseStream);
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
            var request = new HttpRequestMessage(HttpMethod.Get, $"Order/GetById/{id}");

            var client = _clientFactory.CreateClient("myapi");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                Order = JsonConvert.DeserializeObject<OrderGetByIdVM>(responseStream);
            }
            else
            {
                GetPullRequestsError = true;
                Order = null;
            }

            return View(Order);
        }

        [HttpPost]
        public async Task<ActionResult> CompleteOrder(int id, bool state)
        {
            var type = new { Id = id, State = state };

            var content = JsonSerializer.Serialize(type);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);

            var client = _clientFactory.CreateClient("myapi");

            HttpResponseMessage response = await client.PostAsync("Order/CompleteOrder", byteContent);

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
}
