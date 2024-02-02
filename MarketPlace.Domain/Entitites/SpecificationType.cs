using MarketPlace.Domain.Common;

namespace MarketPlace.Domain.Entitites
{
    /// <summary>
    /// This class represents 
    /// </summary>
    public class SpecificationType : AuditableEntity
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public virtual ICollection<Specification>? Specifications { get; set; }
        public virtual ICollection<Category>? Categories { get; set; }
    }
}
