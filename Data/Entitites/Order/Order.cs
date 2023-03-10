
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Data.Entitites
{
    public class Order : BaseEntity
    {
        [Required]
        public PaymentType PaymentType { get; set; }

        public bool IsCompleted { get; set; }
        
        public double TotalPrice { get; set; }

        public int OrderDetailUserId { get; set; }

        [BindNever]
        public OrderDetailUser? OrderDetailUser { get; set; }

        [BindNever]
        public IEnumerable<OrderDetailProduct>? OrderDetailProduct { get; set; }
    }

    public enum PaymentType
    {
        ByCash,
        ByCard,
    }
}
