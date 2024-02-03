using MarketPlace.Domain.Entitites.OrderNS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketPlace.Persistence.Configurations.OrderNS;

public class OrderDetailProductConfiguration : IEntityTypeConfiguration<OrderDetailProduct>
{
    public void Configure(EntityTypeBuilder<OrderDetailProduct> builder)
    {
        builder
            .Property(x => x.Quantity)
            .IsRequired();

        builder
            .Property(x => x.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder
            .HasOne(x => x.Product)
            .WithMany(x => x.OrderDetailProducts)
            .HasForeignKey(x => x.ProductId);

        builder
            .HasOne(x => x.Order)
            .WithMany(x => x.OrderDetailProducts)
            .HasForeignKey(x => x.OrderId);
    }
}
