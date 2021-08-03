using ApplicationService.DTOs.OrderManagementDTOs.GetById;
using Data.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAdminMVC.ViewModels.Order
{
    public class OrderGetByIdVM
    {
        public int Id { get; set; }

        public PaymentType PaymentType { get; set; }

        public string FullName { get; set; }

        public string Phone { get; set; }

        public List<OrderDetailProductByIdDTO> OrderDetailProducts { get; set; }
    }
}
