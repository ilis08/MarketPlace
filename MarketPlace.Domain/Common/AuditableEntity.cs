namespace MarketPlace.Domain.Common;

public abstract class AuditableEntity
{
    public long CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public long UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
