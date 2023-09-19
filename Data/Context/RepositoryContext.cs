using Data.Entitites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Data.Context;

public class RepositoryContext : IdentityDbContext<ApplicationUser, IdentityRole<long>, long>
{
    public DbSet<Product>? Products { get; set; }
    public DbSet<Review>? Reviews { get; set; }
    public DbSet<Category>? Categories { get; set; }
    public DbSet<Order>? Orders { get; set; }
    public DbSet<OrderDetailProduct>? OrderDetailProducts { get; set; }
    public DbSet<Seller> Sellers { get; set; }

    public RepositoryContext(DbContextOptions<RepositoryContext> options)
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

    public RepositoryContext()
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Product>()
        .HasMany(p => p.Reviews)
        .WithOne(r => r.Product)
        .HasForeignKey(r => r.ProductId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Order>()
        .HasMany(p => p.OrderDetailProduct)
        .WithOne(r => r.Order)
        .HasForeignKey(r => r.OrderId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Product>()
        .HasMany(p => p.OrderDetails)
        .WithOne(r => r.Product)
        .HasForeignKey(r => r.ProductId)
        .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(builder);
    }
}
