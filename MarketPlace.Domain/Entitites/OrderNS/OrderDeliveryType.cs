using MarketPlace.Domain.Common;

namespace MarketPlace.Domain.Entitites.OrderNS;

public class OrderDeliveryType : AuditableEntity
{
    public long Id { get; set; }
    public string Title { get; set; } = default!;
    public virtual ICollection<Order>? Orders { get; set; }
}