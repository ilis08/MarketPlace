using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Data.Entitites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using StoreAdminMVC.ViewModels;
using StoreMVC.Models.Order;

namespace StoreMVC.Pages
{
    public class CartModel : PageModel
    {
        public Cart Cart { get; set; }
        private readonly IHttpClientFactory clientFactory;

        public CartModel(IHttpClientFactory factory)
        {
            clientFactory = factory;
        }

        public void OnGet()
        {

        }

        public async Task OnPostAsync(int id)
        {
            ProductVM Product = new ProductVM();

            var request = new HttpRequestMessage(HttpMethod.Get, $"Product/GetById/{id}");

            var client = clientFactory.CreateClient("myapi");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                Product = JsonConvert.DeserializeObject<ProductVM>(responseStream);
            }
            else
            {
                Product = new ProductVM();
            }

            Cart.AddToCart(Product);
        }
    }
}
