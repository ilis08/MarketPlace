using Microsoft.AspNetCore.Mvc;
using StoreMVC.Models.Order;

namespace StoreMVC.Components;

public class CartSummaryViewComponent : ViewComponent
{
    private Cart cart;

    public CartSummaryViewComponent(Cart _cart)
    {
        cart = _cart;
    }

    public IViewComponentResult Invoke()
    {
        return View(cart);
    }
}
