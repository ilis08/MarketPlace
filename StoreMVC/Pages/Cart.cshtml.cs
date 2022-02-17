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
using StoreMVC.Service;

namespace StoreMVC.Pages
{
    [ValidateAntiForgeryToken]
    public class CartModel : PageModel
    {
        public Cart Cart { get; set; }

        private readonly IProductService productService;

        public CartModel(IProductService product, Cart cart)
        {
            productService = product;
            Cart = cart;
        }

        public void OnGet()
        {
        }

        public async Task OnPostAsync(int id)
        {
            var product = await productService.GetProductAsync(id);

            Cart.AddToCart(product);
        }

        public async Task OnPostRemoveAsync(int id)
        {
            var product = await productService.GetProductAsync(id);

            Cart.RemoveFromCart(product);
        }

        public async Task OnPostMinusAsync(int id)
        {
            var cartLine = Cart.Lines.Where(p => p.Product.Id == id).FirstOrDefault();

            if (cartLine.Count > 1)
            {
                var product = await productService.GetProductAsync(id);

                Cart.MinusCount(product);
            }
            else
            {
                var product = await productService.GetProductAsync(id);

                Cart.RemoveFromCart(product);
            }
            
        }
    }
}
