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
        public DbSet<Source> Sources { get; set; }
        public DbSet<Category> Categories { get; set; }

        public DbSet<Evidence> Evidences { get; set; }
        // Added to support logging user searches and basic user lookup
        public DbSet<User> Users { get; set; }
        public DbSet<SearchLog> SearchLogs { get; set; }
    }
}