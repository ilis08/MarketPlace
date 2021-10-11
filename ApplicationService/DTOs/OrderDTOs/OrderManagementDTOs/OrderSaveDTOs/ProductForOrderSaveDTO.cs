using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DTOs.OrderManagementDTOs.OrderSaveDTOs
{
    public class ProductForOrderSaveDTO
    {
        public int Id { get; set; }
        [BindNever]
        public string ProductName { get; set; }
        [BindNever]
        public double Price { get; set; }
    }
}
