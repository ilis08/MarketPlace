using MarketPlace.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketPlace.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder
        .HasMany(p => p.OrderDetailProduct)
        .WithOne(r => r.Order)
        .HasForeignKey(r => r.OrderId)
        .OnDelete(DeleteBehavior.Restrict);
    }
}
