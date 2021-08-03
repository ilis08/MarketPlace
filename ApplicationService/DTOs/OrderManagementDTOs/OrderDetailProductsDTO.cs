using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DTOs.OrderManagementDTOs
{
    public class OrderDetailProductsDTO
    { 
        public int Id { get; set; }
        [Required]
        public int Count { get; set; }

        public int ProductId { get; set; }
    }
}
