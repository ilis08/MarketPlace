using MarketPlace.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketPlace.Persistence.Configurations;

public class SpecificationConfiguration : IEntityTypeConfiguration<Specification>
{
    public void Configure(EntityTypeBuilder<Specification> builder)
    {
        builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(300);

        builder
            .HasOne(x => x.SpecificationType)
            .WithMany(x => x.Specifications)
            .HasForeignKey(x => x.SpecificationTypeId);
    }
}
