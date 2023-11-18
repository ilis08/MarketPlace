using MarketPlace.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketPlace.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.Property(c => c.Title)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(4000);

        builder.Property(c => c.Image)
            .IsRequired();
    }
}
