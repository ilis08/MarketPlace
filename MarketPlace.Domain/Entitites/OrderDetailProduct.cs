using MarketPlace.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace MarketPlace.Domain.Entitites;

public class OrderDetailProduct : AuditableEntity
{
    public Guid Id { get; set; }
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
