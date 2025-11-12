
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CiteTrustApp.Models
{
    public class MyCitation
    {
        [Key]
        public int Id { get; set; }

        // We store email to link to AspNetUsers per your current design.
        // Recommendation: consider adding AspNetUserId (string) and FK to AspNetUsers(Id) instead.
        [StringLength(256)]
        public string Email { get; set; }

        // Store the citation HTML or text (use NVARCHAR(MAX)).
        public string Html { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}