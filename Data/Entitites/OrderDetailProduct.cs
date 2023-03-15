using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Data.Entitites
{
    public class OrderDetailProduct : BaseEntity
    {
        [Required]
        public int Count { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public long OrderId { get; set; }
        public Order Order { get; set; }
    }
}
