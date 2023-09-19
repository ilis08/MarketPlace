using Exceptions.NotFound;
using Newtonsoft.Json;
using StoreMVC.ViewModels;
using StoreMVC.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StoreMVC.Service;

public class ProductService : IProductService
{
    private readonly IHttpClientFactory clientFactory;

    public ProductService(IHttpClientFactory _factory)
    {
        clientFactory = _factory;
    }

    public async Task<ProductVM> GetProductAsync(int id)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"Product/GetById/{id}");

        var client = clientFactory.CreateClient("myapi");

        var response = await client.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            var responseStream = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<ProductVM>(responseStream);
            return product;
        }
        else
        {
            throw new NotFoundException(id, nameof(ProductVM));
        }
    }

    public async Task<List<ProductVM>> GetProductsAsync(string query)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"Product/Get?query={query}");

        var client = clientFactory.CreateClient("myapi");

        var response = await client.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            var responseStream = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<ProductVM>>(responseStream);
            return products;
        }
        else
        {
            return Enumerable.Empty<ProductVM>().ToList();
        }
    }

    public async Task<ProductListVM> GetProductsByParams(ProductParameters productsParams)
    {
        var client = clientFactory.CreateClient("myapi");

        var response = await client.GetAsync($"Product/GetProductsByParams?ProductName={productsParams.ProductName}&Category={productsParams.Category}&Ordering={productsParams.Ordering}&PageSize={productsParams.PageSize}&PageNumber={productsParams.PageNumber}");

        if (response.IsSuccessStatusCode)
        {
            var productListVM = new ProductListVM();

            productListVM.Products = JsonConvert.DeserializeObject<List<ProductVM>>(await response.Content.ReadAsStringAsync()); 
            productListVM.Params = productsParams;
            productListVM.Params.RequestMetaData = JsonConvert.DeserializeObject<RequestMetaData>(response.Headers.GetValues("X-Pagination").FirstOrDefault());

            return productListVM;
        }
        else
        {
            return null;
        }
    }
}
