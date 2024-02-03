using MarketPlace.Domain.Common;

namespace MarketPlace.Domain.Entitites;

public class Review : AuditableEntity
{
    public Guid Id { get; set; }
    public Rating Rating { get; set; }
    public string? Heading { get; set; }
    public string? ReviewText { get; set; }
    public long ProductId { get; set; }
    public Product Product { get; set; } = default!;
}

public enum Rating
{
    OneStar = 1,
    TwoStars = 2,
    ThreeStars = 3,
    FourStars = 4,
    FiveStars = 5
}
