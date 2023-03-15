using System.ComponentModel.DataAnnotations;

namespace Data.Entitites
{
    public class Category : BaseEntity
    {
        [Required]
        [MinLength(2)]
        [MaxLength(20)]
        public string? Title { get; set; }
        [Required]
        [MinLength(5)]
        [MaxLength(100)]
        public string? Description { get; set; }
        [Required]
        public string Image { get; set; }

        public virtual ICollection<Product>? Products { get; set; }
    }
}
