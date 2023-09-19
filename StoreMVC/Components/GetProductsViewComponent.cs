using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoreMVC.ViewModels;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace StoreMVC.Components;

public class GetProductsViewComponent : ViewComponent
{
    private IHttpClientFactory clientFactory;

    public GetProductsViewComponent(IHttpClientFactory _factory)
    {
        clientFactory = _factory;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var client = clientFactory.CreateClient("myapi");

        var response = await client.GetAsync($"Product/Get");

        var products = JsonConvert.DeserializeObject<IEnumerable<ProductVM>>(await response.Content.ReadAsStringAsync());

        return View(products);
    }
}
