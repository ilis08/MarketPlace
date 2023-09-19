using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Data.Entitites;

public class Order : BaseEntity
{
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
