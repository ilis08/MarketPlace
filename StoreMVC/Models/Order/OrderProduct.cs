using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreMVC.Models.Order;

public class OrderProduct
{
    public int Id { get; set; }

    public string ProductName { get; set; }

    public double Price { get; set; }

    public string Image { get; set; }

}
