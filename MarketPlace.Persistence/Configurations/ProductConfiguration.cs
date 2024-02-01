using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MarketPlace.Domain.Entitites;

namespace MarketPlace.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
        .HasMany(p => p.Reviews)
        .WithOne(r => r.Product)
        .HasForeignKey(r => r.ProductId)
        .OnDelete(DeleteBehavior.Restrict);

        builder
        .HasMany(p => p.OrderDetails)
        .WithOne(r => r.Product)
        .HasForeignKey(r => r.ProductId)
        .OnDelete(DeleteBehavior.Restrict);
    }
}
