using MarketPlace.Domain.Entitites.OrderNS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketPlace.Persistence.Configurations.OrderNS;

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

        builder
            .HasOne(x => x.OrderDeliveryType)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.OrderDeliveryType)
            .IsRequired();

        builder
            .HasOne(x => x.Customer)
            .WithMany(x => x.Orders)
            .HasForeignKey(x => x.CustomerId);

        builder
            .HasOne(x => x.OrderUserInformation)
            .WithOne(x => x.Order)
            .HasForeignKey<OrderUserInformation>(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
