using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Data.Entitites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using StoreMVC.ViewModels;
using StoreMVC.Models.Order;
using StoreMVC.Models.Order.SessionCart;

namespace StoreMVC.Pages
{
    [ValidateAntiForgeryToken]
    public class CartModel : PageModel
    {
        public Cart Cart { get; set; }
        private readonly IHttpClientFactory clientFactory;

        public ProductVM Product { get; set; }

        public CartModel(IHttpClientFactory factory, Cart cart)
        {
            clientFactory = factory;
            Cart = cart;
        }

        public void OnGet()
        {
        }

        public async Task OnPostAsync(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"Product/GetById/{id}");

            var client = clientFactory.CreateClient("myapi");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                Product = JsonConvert.DeserializeObject<ProductVM>(responseStream);
            }

            Cart.AddToCart(Product);
        }

        public async Task OnPostRemoveAsync(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"Product/GetById/{id}");

            var client = clientFactory.CreateClient("myapi");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                Product = JsonConvert.DeserializeObject<ProductVM>(responseStream);
            }

            Cart.RemoveFromCart(Product);

        }

        public async Task OnPostMinusAsync(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"Product/GetById/{id}");

            var client = clientFactory.CreateClient("myapi");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                Product = JsonConvert.DeserializeObject<ProductVM>(responseStream);
            }

            Cart.MinusCount(Product);
        }
    }
}
