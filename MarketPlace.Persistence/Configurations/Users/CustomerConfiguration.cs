using MarketPlace.Domain.Entitites.UsersNS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketPlace.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder
            .Property(x => x.Appeal)
            .HasMaxLength(100);

        builder
            .Property(x => x.BirthDay)
            .HasColumnType("date");
    }
}
