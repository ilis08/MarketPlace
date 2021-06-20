using Data.Entitites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Context
{
    public class StoreDBContext : DbContext
    {

        public DbSet<Phone> Phones { get; set; }
        public DbSet<Category> Categories { get; set; }

        public StoreDBContext()
        {

        }
        public StoreDBContext(DbContextOptions<StoreDBContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=IlisStoreEntitiesDbContext;Trusted_Connection=True;");
        }
    }
}
