namespace CiteTrustApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Source
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Source()
        {
            Evidences = new HashSet<Evidence>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(100)]
        public string Author { get; set; }

        public int? Year { get; set; }

        [StringLength(100)]
        public string Publisher { get; set; }

        [StringLength(255)]
        public string Url { get; set; }

        public decimal? CreditScore { get; set; }

        [StringLength(50)]
        public string SourceType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Evidence> Evidences { get; set; }
    }
}
