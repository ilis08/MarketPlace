using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entitites
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public PaymentType PaymentType { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        public DateTime OrderTime { get; set; }

        public bool IsCompleted { get; set; }

        public double TotalPrice { get; set; }

        [BindNever]
        public OrderDetailUser OrderDetailUser { get; set; }

        [BindNever]
        public IEnumerable<OrderDetailProduct> OrderDetailProduct { get; set; }


        public IEnumerable<ValidationResult> ValidatePaymentType(ValidationContext validationContext)
        {
            if (PaymentType == PaymentType.None)
            {
                yield return new ValidationResult("Payment type is not selected");
            }
        }
    }

    public enum PaymentType
    {
        None,
        ByCash,
        ByCard,
    }
}
