using MarketPlace.Domain.Common;
using MarketPlace.Domain.Entitites;
using MarketPlace.Domain.Entitites.OrderNS;
using MarketPlace.Domain.Entitites.UsersNS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace MarketPlace.Persistence;

public class MarketPlaceDbContext : DbContext
{
    public DbSet<Admin>? Admins { get; set; }
    public DbSet<Customer>? Customers { get; set; }
    public DbSet<Seller>? Sellers { get; set; }
    public DbSet<Product>? Products { get; set; }
    public DbSet<Review>? Reviews { get; set; }
    public DbSet<Category>? Categories { get; set; }
    public DbSet<SpecificationType>? SpecificationTypes { get; set; }
    public DbSet<Specification>? Specifications { get; set; }
    public DbSet<Image>? Images { get; set; }
    public DbSet<Order>? Orders { get; set; }
    public DbSet<OrderDetailProduct>? OrderDetailProducts { get; set; }
    public DbSet<OrderUserInformation>? OrderUserInformations { get; set; }

    public MarketPlaceDbContext(DbContextOptions<MarketPlaceDbContext> options)
        : base(options)
    {
        var dbCreater = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
        if (dbCreater != null)
        {
            // Create Database 
            if (!dbCreater.CanConnect())
            {
                dbCreater.Create();
            }

            // Create Tables
            if (!dbCreater.HasTables())
            {
                dbCreater.CreateTables();
            }
        }
    }

    public MarketPlaceDbContext()
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof
            (MarketPlaceDbContext).Assembly);


        base.OnModelCreating(builder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Modified:
                    entry.Entity.CreatedDate = DateTime.Now;
                    break;
                case EntityState.Added:
                    entry.Entity.CreatedDate = DateTime.Now;
                    break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
