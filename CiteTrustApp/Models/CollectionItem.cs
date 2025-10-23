namespace CiteTrustApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CollectionItem
    {
        public int Id { get; set; }

        public int? CollectionId { get; set; }

        public int? EvidenceId { get; set; }

        public DateTime? AddedAt { get; set; }

        public virtual Collection Collection { get; set; }

        public virtual Evidence Evidence { get; set; }
    }
}
