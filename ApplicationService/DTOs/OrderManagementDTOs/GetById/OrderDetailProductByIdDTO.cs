using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationService.DTOs.OrderManagementDTOs.GetById
{
    public class OrderDetailProductByIdDTO
    {
        public string ProductName { get; set; }

        public int Count { get; set; }
    }
}
