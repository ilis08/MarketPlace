using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using StoreAdminMVC.ViewModels;

namespace StoreMVC.Pages
{
    public class CategoriesModel : PageModel
    {
        private readonly IHttpClientFactory clientFactory;
        public IEnumerable<CategoryVM> Categories { get; private set; }

        public CategoriesModel(IHttpClientFactory factory)
        {
            clientFactory = factory;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "Category/Get/");

            var client = clientFactory.CreateClient("myapi");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                Categories = JsonConvert.DeserializeObject<List<CategoryVM>>(responseStream);
            }
            else
            {
                Categories = Array.Empty<CategoryVM>();
            }

            return Page();
        }
    }
}
