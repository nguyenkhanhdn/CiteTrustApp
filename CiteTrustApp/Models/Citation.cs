namespace CiteTrustApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Citation
    {
        public int Id { get; set; }

        public int? EvidenceId { get; set; }

        [StringLength(50)]
        public string FormatType { get; set; }

        public string CitationText { get; set; }

        public virtual Evidence Evidence { get; set; }
    }
}
