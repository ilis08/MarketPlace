using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoreMVC.ViewModels;
using StoreMVC.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StoreMVC.Components
{
    public class GetProductsByParamsViewComponent : ViewComponent
    {
        private IHttpClientFactory clientFactory;

        public GetProductsByParamsViewComponent(IHttpClientFactory _factory)
        {
            clientFactory = _factory;
        }

        public async Task<IViewComponentResult> InvokeAsync(GetProductsParams productsParams)
        {
            var client = clientFactory.CreateClient("myapi");

            var response = await client.GetAsync($"Product/GetProductsByParams?Category={productsParams.Category}&Ordering={productsParams.Ordering}");

            var products = JsonConvert.DeserializeObject<IEnumerable<ProductVM>>(await response.Content.ReadAsStringAsync());

            ProductListVM productList = new();

            productList.Params = productsParams;
            productList.Products = products;

            return View(productList);
        }
    }
}
