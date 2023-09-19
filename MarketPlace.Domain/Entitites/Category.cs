using System.ComponentModel.DataAnnotations;
using MarketPlace.Domain.Common;

namespace MarketPlace.Domain.Entitites;

public class Category : AuditableEntity<long>
{
    [Required]
    [MinLength(2)]
    [MaxLength(20)]
    public string Title { get; set; }
    [Required]
    [MinLength(5)]
    [MaxLength(100)]
    public string? Description { get; set; }
    [Required]
    public string Image { get; set; }

    public virtual ICollection<Product>? Products { get; set; }
}
