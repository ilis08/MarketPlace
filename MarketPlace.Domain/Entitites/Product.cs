using System.ComponentModel.DataAnnotations;
using MarketPlace.Domain.Common;

namespace MarketPlace.Domain.Entitites;

public class Product : AuditableEntity
{
    public Guid Id { get; set; }
    [Required]
    [StringLength(100)]
    public string? ProductName { get; set; }
    [Required]
    [StringLength(500)]
    public string? Description { get; set; }
    [Required]
    public DateTime? Release { get; set; }
    [Required]
    public double Price { get; set; }
    [Required]
    public int Quantity { get; set; }
    public string? Image { get; set; }
    [Required]
    public long CategoryId { get; set; }
    public Category Category { get; set; }
    public long SellerId { get; set; }
    public Seller Seller { get; set; }
    public virtual ICollection<Review>? Reviews { get; set; }
    public virtual ICollection<OrderDetailProduct>? OrderDetails { get; set; }
}
