using Data.Entitites;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using StoreMVC.Models;
using StoreMVC.Models.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreMVC.ViewModels
{
    public class OrderVM
    {
        public int OrderId { get; set; }
        [DataType(DataType.CreditCard)]
        public PaymentType PaymentType { get; set; }

        public double TotalPrice { get; set; }

        [Required(ErrorMessage = "Please, enter the user")]
        public OrderUserVM OrderDetailUser { get; set; }
        [Required]
        public List<CartLine> OrderDetailProducts { get; set; }
    }
}
