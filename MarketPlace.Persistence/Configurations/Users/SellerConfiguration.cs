using MarketPlace.Domain.Entitites.UsersNS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketPlace.Persistence.Configurations;

public class SellerConfiguration : IEntityTypeConfiguration<Seller>
{
    public void Configure(EntityTypeBuilder<Seller> builder)
    {
        builder.Property(c => c.CompanyName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.FullCompanyName)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(c => c.CommonInformation)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(c => c.PhoneNumber)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(c => c.Bank)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.IBAN)
           .IsRequired()
           .HasMaxLength(34);
    }
}
