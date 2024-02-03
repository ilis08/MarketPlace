using MarketPlace.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketPlace.Persistence.Configurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder
            .Property(x => x.Rating)
            .IsRequired();

        builder
            .Property(x => x.Heading)
            .HasMaxLength(200);

        builder
            .Property(x => x.ReviewText)
            .HasMaxLength(7000);

        builder
            .HasOne(x => x.Product)
            .WithMany(x => x.Reviews)
            .HasForeignKey(x => x.ProductId);
    }
}
