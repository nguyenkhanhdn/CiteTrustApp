namespace CiteTrustApp.Models
{
    using System;

    public class CollectionItem
    {
        public int Id { get; set; }

        public int? CollectionId { get; set; }

        public int? EvidenceId { get; set; }

        public DateTime? AddedAt { get; set; }

        public virtual Collection Collection { get; set; }

        public virtual Evidence Evidence { get; set; }
    }
}
