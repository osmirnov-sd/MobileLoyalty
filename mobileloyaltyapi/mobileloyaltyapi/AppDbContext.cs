using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using mobileloyaltyapi.Models;

namespace mobileloyaltyapi
{

    public class AppDbContext : DbContext
    {
        public DbSet<Entity> Entity { get; set; }

        public AppDbContext() : base(nameOrConnectionString: "Default") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            base.OnModelCreating(modelBuilder);
        }
    }

}