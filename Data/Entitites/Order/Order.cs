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
        [BindNever]
        public int Id { get; set; }

        [Required]
        public PaymentType PaymentType { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]
        public DateTime OrderTime { get; set; }

        public OrderDetailUser OrderDetailUser { get; set; }

        public ICollection<OrderDetailProduct> OrderDetailProduct { get; set; }


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
