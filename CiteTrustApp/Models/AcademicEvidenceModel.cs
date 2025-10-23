using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace CiteTrustApp.Models
{
    public partial class AcademicEvidenceModel : DbContext
    {
        public AcademicEvidenceModel()
            : base("name=AcademicEvidenceDb")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Citation> Citations { get; set; }
        public virtual DbSet<CollectionItem> CollectionItems { get; set; }
        public virtual DbSet<Collection> Collections { get; set; }
        public virtual DbSet<Evidence> Evidences { get; set; }
        public virtual DbSet<SearchLog> SearchLogs { get; set; }
        public virtual DbSet<Source> Sources { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Source>()
                .Property(e => e.CreditScore)
                .HasPrecision(3, 2);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Evidences)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.CreatedBy);

            modelBuilder.Entity<Evidence>()
                .HasOptional(e => e.User)
                .WithMany(u => u.Evidences)
                .HasForeignKey(e => e.CreatedBy);

            base.OnModelCreating(modelBuilder);
        }
      
    }
}
