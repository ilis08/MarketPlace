using StoreMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreMVC.Models.Order;

public class Cart
{
    public List<CartLine> Lines { get; set; } = new List<CartLine>();

    public virtual void AddToCart(ProductVM product)
    {
        CartLine line = Lines.Where(p => p.Product.Id == product.Id).FirstOrDefault();

        if (line == null)
        {
            Lines.Add(new CartLine
            {
                Product = product,
                Count = 1
            });
        }
        else
        {
            line.Count += 1;
        }
    }

    public virtual void RemoveFromCart(ProductVM product)
    {
        Lines.RemoveAll(p => p.Product.Id == product.Id);
    }

    public virtual void MinusCount(ProductVM product)
    {
        foreach (var item in Lines.Where(p => p.Product.Id == product.Id))
        {
            item.Count -= 1;
        }
    }

    public double ComputeTotalPrice()
    {
        return Lines.Sum(e => e.Count * e.Product.Price);
    }

    public virtual void CleanCart()
    {
        Lines.Clear();
    }
}
