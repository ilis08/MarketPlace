using MarketPlace.Domain.Entitites.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketPlace.Persistence.Configurations;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder
            .Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(200);

        builder
            .Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(200);
    }
}
