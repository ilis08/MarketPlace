using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoreMVC.ViewModels;
using StoreMVC.ViewModels.Product;

namespace StoreMVC.Components;

public class NavigationCategoryViewComponent : ViewComponent
{
    private IHttpClientFactory clientFactory;

    public NavigationCategoryViewComponent(IHttpClientFactory _factory)
    {
        clientFactory = _factory;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var client = clientFactory.CreateClient("myapi");

        var response = await client.GetAsync("Category/Get");

        var category = JsonConvert.DeserializeObject<IEnumerable<CategoryVM>>(await response.Content.ReadAsStringAsync());

        ViewBag.SelectedCategory = RouteData?.Values["category"];

        ProductListVM product = new();

        product.Categories = category;

        return View(product);
    }
}
