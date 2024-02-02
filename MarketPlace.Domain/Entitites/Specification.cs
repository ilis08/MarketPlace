using MarketPlace.Domain.Common;

namespace MarketPlace.Domain.Entitites
{
    public class Specification : AuditableEntity
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public long SpecificationTypeId { get; set; }
        public SpecificationType? SpecificationType { get; set; }
        public virtual ICollection<Product>? Products { get; set; }
    }
}
