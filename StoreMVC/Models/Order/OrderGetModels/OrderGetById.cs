using ApplicationService.DTOs.OrderManagementDTOs.GetById;
using Data.Entitites;
using System.Collections.Generic;

namespace StoreMVC.Models.Order.OrderGetModels
{
    public class OrderGetById
    {
        public int Id { get; set; }

        public PaymentType PaymentType { get; set; }

        public bool IsCompleted { get; set; }

        public double TotalPrice { get; set; }

        public string FullName { get; set; }

        public string Phone { get; set; }
    }
}
