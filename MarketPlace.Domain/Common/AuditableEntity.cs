namespace MarketPlace.Domain.Common;

public abstract class AuditableEntity<TType> where TType : struct
{
    public TType Id { get; set; }
    public long CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public long UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
}
