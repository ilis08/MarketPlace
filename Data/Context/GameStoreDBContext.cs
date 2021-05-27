using Data.Entitites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Context
{
    public class GameStoreDBContext : DbContext
    {
        public GameStoreDBContext()
        {

        }
        public GameStoreDBContext(DbContextOptions<GameStoreDBContext> options)
            : base(options)
        {

        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
