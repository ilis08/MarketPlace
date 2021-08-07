using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using StoreMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StoreMVC.Models.Order.SessionCart
{
    public class CartSession : Cart
    {
       
        public static Cart GetCart(IServiceProvider service)
        {
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            CartSession cart = session?.GetJson<CartSession>("Cart") ?? new CartSession();

            cart.Session = session;

            return cart;
        }

        [JsonIgnore]
        public ISession Session { get; set; }


        public override void AddToCart(ProductVM product)
        {
            base.AddToCart(product);
            Session.SetJson("Cart", this);
        }
    }
}
