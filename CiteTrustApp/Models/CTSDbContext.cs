using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CiteTrustApp.Models
{
    public class CTSDbContext:DbContext
    {
        public CTSDbContext() : base("name=CTSDbContext")
        {
        }
        public virtual DbSet<Source> Sources { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Evidence> Evidences { get; set; }

        public virtual DbSet<Citation>  Citations { get; set; }
        // Ensure these DbSet properties exist for the recommendation service
        public virtual DbSet<Collection> Collections { get; set; }
        public virtual DbSet<CollectionItem> CollectionItems { get; set; }

        // Added to support logging user searches and basic user lookup
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<SearchLog> SearchLogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // preserve any existing configuration
        }
    }
}