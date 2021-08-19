using Data.Entitites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Context
{
    public class Store3DBContext : DbContext
    {

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetailProduct> OrderDetailProducts { get; set; }
        public DbSet<OrderDetailUser> OrderDetailUsers { get; set; }

        public Store3DBContext()
        {
        }
        public Store3DBContext(DbContextOptions<Store3DBContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source = tcp:ilisstoredb.database.windows.net, 1433; Initial Catalog = IlisStoreDB; User Id = iliya@ilisstoredb; Password = gsrk$T173bv");
        }
    }
}
