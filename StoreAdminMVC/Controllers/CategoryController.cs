using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StoreAdminMVC.Filters;
using StoreAdminMVC.Services;
using StoreAdminMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;


namespace StoreAdminMVC.Controllers
{
    [ResourceFilter]
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory clientFactory;
        private ILogger<CategoryController> logger;

        public CategoryController(ILogger<CategoryController> _logger, IHttpClientFactory _client)
        {
            logger = _logger;
            clientFactory = _client;
        }

        public async Task<ActionResult> Index(string query)
        {
            var client = clientFactory.CreateClient("myapi");

            HttpResponseMessage response = await client.GetAsync("Category/Get");

            var responseData = JsonConvert.DeserializeObject<List<CategoryVM>>(await response.Content.ReadAsStringAsync());

            return View(responseData);

        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CategoryVM categoryVM)
        {
            try
            {
                var client = clientFactory.CreateClient("myapi");

                HttpResponseMessage response = await client.PostAsJsonAsync("Category/Save", categoryVM);

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var client = clientFactory.CreateClient("myapi");

            var response = await client.GetAsync("Category/GetById/" + id);

            var responseData = JsonConvert.DeserializeObject<CategoryVM>(await response.Content.ReadAsStringAsync());

            return View(responseData);
        }     

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CategoryVM categoryVM)
        {
            var client = clientFactory.CreateClient("myapi");

            var response = await client.PostAsJsonAsync("Category/Save", categoryVM);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Details(int id)
        {
            var client = clientFactory.CreateClient("myapi");

            var response = await client.GetAsync("Category/GetById/" + id);

            var body = JsonConvert.DeserializeObject<CategoryVM>(await response.Content.ReadAsStringAsync());

            return View(body);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var client = clientFactory.CreateClient("myapi");

            var response = await client.GetAsync("Category/GetById/" + id);

            var responseData = JsonConvert.DeserializeObject<CategoryVM>(await response.Content.ReadAsStringAsync());

            return View(responseData);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var client = clientFactory.CreateClient("myapi");

            var response = await client.DeleteAsync("Category/Delete/" + id);

            return RedirectToAction("Index");
        }
    }
}
