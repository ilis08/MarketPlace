using MarketPlace.Domain.Common;
using MarketPlace.Domain.Entitites.OrderNS;
using MarketPlace.Domain.Entitites.UsersNS;

namespace MarketPlace.Domain.Entitites;

public class Product : AuditableEntity
{
    public long Id { get; set; }
    public string ProductName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public DateOnly? ReleaseDate { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public long LogoImageId { get; set; }
    public Image Image { get; set; } = default!;
    public long CategoryId { get; set; }
    public Category Category { get; set; } = default!;
    public long SellerId { get; set; }
    public Seller Seller { get; set; } = default!;
    public virtual ICollection<Review>? Reviews { get; set; }
    public virtual ICollection<OrderDetailProduct>? OrderDetailProducts { get; set; }
    public virtual ICollection<Specification>? Specifications { get; set; }
    public virtual ICollection<Image> Images { get; set; } = new List<Image>();
}
