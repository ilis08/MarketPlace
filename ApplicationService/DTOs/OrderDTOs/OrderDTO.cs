
using ApplicationService.DTOs.OrderManagementDTOs;
using Data.Entitites;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }

        public PaymentType PaymentType { get; set; } 

        public double TotalPrice { get; set; }

        public OrderDetailUser OrderDetailUser { get; set; }

        public IEnumerable<OrderDetailProductsDTO> OrderDetailProducts { get; set; }

    }
}
