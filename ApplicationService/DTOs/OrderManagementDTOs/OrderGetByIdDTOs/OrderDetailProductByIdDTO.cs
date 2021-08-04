using Data.Entitites;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DTOs.OrderManagementDTOs.GetById
{
    public class OrderDetailProductByIdDTO
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int Count { get; set; }

        public double Price { get; set; }
    }
}
