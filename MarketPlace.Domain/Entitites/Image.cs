using MarketPlace.Domain.Common;

namespace MarketPlace.Domain.Entitites
{
    public class Image : AuditableEntity
    {
        public long Id { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
        public virtual ICollection<Category>? Categories { get; set; }
    }
}
