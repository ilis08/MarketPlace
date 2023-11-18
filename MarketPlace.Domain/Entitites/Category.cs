using MarketPlace.Domain.Common;

namespace MarketPlace.Domain.Entitites;

public class Category : AuditableEntity
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public virtual ICollection<Product>? Products { get; set; }
}
