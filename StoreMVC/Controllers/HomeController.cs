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
        private IEnumerable<ProductVM> Products { get; set; }
        private ProductVM Product { get; set; }


        public HomeController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<ActionResult> Index()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"Product/Get/");

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

        public void Add(ref int i)
        {
            ++i;
        }
        public void Minus(ref int i)
        {
            --i;
        }


        public async Task<IActionResult> Details(int id)
        {
            var client = _clientFactory.CreateClient("myapi");

            var request = new HttpRequestMessage(HttpMethod.Get, $"Product/GetById/{id}");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                Product = JsonConvert.DeserializeObject<ProductVM>(responseStream);
            }
            else
            {
                return RedirectToAction("Index");
            }

            return View(Product);
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
