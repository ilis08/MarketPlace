using MarketPlace.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace MarketPlace.Domain.Entitites;

public class Review : AuditableEntity<Guid>
{
    [Required]
    public RatingEnum Rating { get; set; }
    [MinLength(1)]
    [MaxLength(4000)]
    public string? Comment { get; set; }
    [Required]
    public long ProductId { get; set; }
    public Product Product { get; set; }
    [Required]
    public long UserId { get; set; }
    public ApplicationUser User { get; set; }
}

public enum RatingEnum
{
    OneStar = 1,
    TwoStars = 2,
    ThreeStars = 3,
    FourStars = 4,
    FiveStars = 5
}
