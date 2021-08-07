using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StoreMVC.Models;
using StoreMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        public IEnumerable<ProductVM> Products { get; private set; }

        private readonly string imagePath = @"C:/DistributedProject/IlisStoreSln/StoreAdminMVC/wwwroot/Images/";

        public HomeController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<ActionResult> Index(string query)
        {
            ViewBag.Image = imagePath;

            var request = new HttpRequestMessage(HttpMethod.Get, "Product/Get/");

            var client = _clientFactory.CreateClient("myapi");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                Products = JsonConvert.DeserializeObject<List<ProductVM>>(responseStream);
            }
            else
            {
                Products = Array.Empty<ProductVM>();
            }


            return View(Products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
