using MarketPlace.Domain.Common;

namespace MarketPlace.Domain.Entitites;

public class Order : AuditableEntity
{
    public Guid Id { get; set; }
    public PaymentType PaymentType { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public decimal TotalPrice { get; set; }
    public virtual ICollection<OrderDetailProduct>? OrderDetailProducts { get; set; }
}

public enum OrderStatus
{
    Pending,
    Approved,
    Delivery,
    Done,
    Cancelled
}

public enum PaymentType
{
    ByCash,
    ByCard,
}
