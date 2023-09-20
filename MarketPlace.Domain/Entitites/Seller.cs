using MarketPlace.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace MarketPlace.Domain.Entitites;

public class Seller : AuditableEntity
{
    public Guid Id { get; set; }
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    [Required]
    [StringLength(500)]
    public string Description { get; set; }
    [Required]
    [Url]
    public string LogoUrl { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    [Phone]
    public string PhoneNumber { get; set; }
    [Url]
    public string Url { get; set; }
    [Required]
    public bool IsActive { get; set; } = true;
    [Required]
    public long UserId { get; set; }
    public ApplicationUser User { get; set; }
    public virtual ICollection<Product>? Products { get; set; }
}
