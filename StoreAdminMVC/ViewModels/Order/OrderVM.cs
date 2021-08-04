using ApplicationService.DTOs.OrderManagementDTOs;
using Data.Entitites;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAdminMVC.ViewModels.Order
{
    public class OrderVM
    {
        public int OrderId { get; set; }

        public PaymentType PaymentType { get; set; }

        public double TotalPrice { get; set; }

        [BindNever]
        public OrderDetailUser OrderDetailUser { get; set; }

        [BindNever]
        public IEnumerable<OrderDetailProductsDTO> OrderDetailProducts { get; set; }

        public IEnumerable<ValidationResult> ValidatePaymentType(ValidationContext validationContext)
        {
            if (PaymentType == PaymentType.None)
            {
                yield return new ValidationResult("Payment type is not selected");
            }
        }

    }
}
