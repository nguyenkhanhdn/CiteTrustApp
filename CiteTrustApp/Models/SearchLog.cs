namespace CiteTrustApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SearchLog
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        [StringLength(255)]
        public string Keyword { get; set; }

        public DateTime? SearchTime { get; set; }

        public virtual User User { get; set; }
    }
}
