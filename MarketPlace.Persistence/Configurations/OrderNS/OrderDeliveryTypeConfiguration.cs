using MarketPlace.Domain.Entitites.OrderNS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketPlace.Persistence.Configurations.OrderNS;

public class OrderDeliveryTypeConfiguration : IEntityTypeConfiguration<OrderDeliveryType>
{
    public void Configure(EntityTypeBuilder<OrderDeliveryType> builder)
    {
        builder
            .Property(x => x.Title)
            .HasMaxLength(100)
            .IsRequired();
    }
}
