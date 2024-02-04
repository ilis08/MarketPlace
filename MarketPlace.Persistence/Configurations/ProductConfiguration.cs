using MarketPlace.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketPlace.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(c => c.ProductName)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(c => c.Description)
            .IsRequired()
            .HasMaxLength(10000);

        builder.Property(c => c.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(c => c.Quantity)
            .IsRequired()
            .HasMaxLength(10000);

        builder
            .HasOne(x => x.Image)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.LogoImageId);

        builder
            .HasMany(x => x.Images)
            .WithMany(x => x.Products);

        builder
            .HasOne(x => x.Category)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.CategoryId);

        builder
            .HasOne(x => x.Seller)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.SellerId);
    }
}
