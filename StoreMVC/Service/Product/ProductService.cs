﻿using Newtonsoft.Json;
using StoreMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace StoreMVC.Service
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory clientFactory;

        public ProductVM Product { get; set; }

        public IEnumerable<ProductVM> Products { get; set; }

        public ProductService(IHttpClientFactory _factory)
        {
            clientFactory = _factory;
        }

        public async Task GetProductAsync(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"Product/GetById/{id}");

            var client = clientFactory.CreateClient("myapi");

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                Product = JsonConvert.DeserializeObject<ProductVM>(responseStream);
            }
        }

        public async Task GetProductsAsync(string query)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"Product/Get?query={query}");

            var client = clientFactory.CreateClient("myapi");

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
        }
    }
}
