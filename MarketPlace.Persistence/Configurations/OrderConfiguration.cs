using MarketPlace.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketPlace.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder
            .Property(x => x.PaymentType)
            .IsRequired();

        builder
            .Property(x => x.OrderStatus)
            .IsRequired();

        builder
            .Property(x => x.TotalPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
    }
}
