using MarketPlace.Domain.Entitites.OrderNS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketPlace.Persistence.Configurations.OrderNS;

public class OrderUserInformationConfiguration : IEntityTypeConfiguration<OrderUserInformation>
{
    public void Configure(EntityTypeBuilder<OrderUserInformation> builder)
    {

        builder
            .Property(x => x.FirstName)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(x => x.LastName)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(x => x.Email)
            .HasMaxLength(320)
            .IsRequired();

        builder
            .Property(x => x.PhoneNumber)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(x => x.InvoiceAddress)
            .HasMaxLength(350)
            .IsRequired();
    }
}
