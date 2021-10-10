using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StoreMVC.Models;
using StoreMVC.Service;
using StoreMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService productService;


        public HomeController(IProductService _productService)
        {
            productService = _productService;
        }

        public async Task<ActionResult> Index()
        {
            await productService.GetProductsAsync();

            return View(productService.Products);
        }

        public async Task<IActionResult> Details(int id)
        { 
            await productService.GetProductAsync(id);

            if (productService.Product.Id == 0)
            {
                return RedirectToAction("Index");
            }

            return View(productService.Product);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
