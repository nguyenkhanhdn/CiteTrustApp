namespace CiteTrustApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Evidence
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Evidence()
        {
            Citations = new HashSet<Citation>();
            CollectionItems = new HashSet<CollectionItem>();
        }

        public int Id { get; set; }

        public string Content { get; set; }

        [StringLength(50)]
        public string Type { get; set; }

        public int? SourceId { get; set; }

        public int? CategoryId { get; set; }


        [ForeignKey("User")]
        public int? CreatedBy { get; set; }

        public virtual User User { get; set; }

   
        public DateTime? CreatedAt { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Citation> Citations { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CollectionItem> CollectionItems { get; set; }
        public virtual Source Source { get; set; }
    }
}
