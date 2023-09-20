using MarketPlace.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace MarketPlace.Domain.Entitites;

public class Order : AuditableEntity
{
    public Guid Id { get; set; }
    [Required]
    public PaymentType PaymentType { get; set; }
    public bool IsCompleted { get; set; }
    public double TotalPrice { get; set; }
    [Required]
    public long UserId { get; set; }
    public ApplicationUser User { get; set; }
    public virtual ICollection<OrderDetailProduct>? OrderDetailProduct { get; set; }
}

public enum PaymentType
{
    ByCash,
    ByCard,
}
