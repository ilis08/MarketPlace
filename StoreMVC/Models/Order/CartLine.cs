using Microsoft.AspNetCore.Mvc.ModelBinding;
using StoreMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreMVC.Models.Order;

public class CartLine
{
    public int CartLineId { get; set; }
    [BindNever]
    public ProductVM Product { get; set; }

    public int Count { get; set; }
}
