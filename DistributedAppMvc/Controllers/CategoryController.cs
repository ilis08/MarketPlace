using DistributedAppMvc.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DistributedAppMvc.Controllers
{
    public class CategoryController : Controller
    {
        private readonly Uri uri = new Uri("http://localhost:41486/api/Category/");

        public async Task<ActionResult> Index(string query)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("get");

                string jsonString = await response.Content.ReadAsStringAsync();
                var responseData = JsonConvert.DeserializeObject<List<CategoryVM>>(jsonString);

                return View(responseData);
            }
        }
    }
}
