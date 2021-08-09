using Data.Entitites;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using StoreMVC.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreMVC.ViewModels
{
    public class OrderVM
    {
        public int OrderId { get; set; }

        public PaymentType PaymentType { get; set; }

        public double TotalPrice { get; set; }

        public OrderDetailUser OrderDetailUser { get; set; }

        public List<CartLine> OrderDetailProducts { get; set; }
    }
}
