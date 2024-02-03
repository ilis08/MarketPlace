using MarketPlace.Domain.Common;
using MarketPlace.Domain.Entitites.UsersNS;

namespace MarketPlace.Domain.Entitites.OrderNS;

public class Order : AuditableEntity
{
    public Guid Id { get; set; }
    public PaymentType PaymentType { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public decimal TotalPrice { get; set; }
    public long OrderDeliveryTypeId { get; set; }
    public OrderDeliveryType OrderDeliveryType { get; set; } = default!;
    public Guid? OrderUserInformationId { get; set; }
    public OrderUserInformation? OrderUserInformation { get; set; }
    public long? CustomerId { get; set; }
    public Customer? Customer { get; set; }
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
