
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

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
        public DateTime OrderTime { get; set; } = DateTime.Now;

        public bool IsCompleted { get; set; }
        
        public double TotalPrice { get; set; }

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
