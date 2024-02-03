using MarketPlace.Domain.Common;

namespace MarketPlace.Domain.Entitites.OrderNS;

public class OrderDetailProduct : AuditableEntity
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public long ProductId { get; set; }
    public Product Product { get; set; } = default!;
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = default!;
}
