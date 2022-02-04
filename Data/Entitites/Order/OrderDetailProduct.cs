using System.ComponentModel.DataAnnotations;

namespace Data.Entitites
{
    public class OrderDetailProduct
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Count { get; set; }

        public int ProductId { get; set; }

        public Product? Product { get; set; }

        public int OrderId { get; set; }

        public double Price { get; set; }
    }
}
