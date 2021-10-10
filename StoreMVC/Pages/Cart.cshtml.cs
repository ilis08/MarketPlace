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

        private readonly IProductService productMethods;

        public CartModel(IProductService product, Cart cart)
        {
            productMethods = product;
            Cart = cart;
        }

        public void OnGet()
        {
        }

        public async Task OnPostAsync(int id)
        {
            await productMethods.GetProductAsync(id);

            Cart.AddToCart(productMethods.Product);
        }

        public async Task OnPostRemoveAsync(int id)
        {
            await productMethods.GetProductAsync(id);

            Cart.RemoveFromCart(productMethods.Product);
        }

        public async Task OnPostMinusAsync(int id)
        {
            var cartLine = Cart.Lines.Where(p => p.Product.Id == id).FirstOrDefault();

            if (cartLine.Count > 1)
            {
                await productMethods.GetProductAsync(id);

                Cart.MinusCount(productMethods.Product);
            }
            else
            {
                await productMethods.GetProductAsync(id);

                Cart.RemoveFromCart(productMethods.Product);
            }
            
        }
    }
}
