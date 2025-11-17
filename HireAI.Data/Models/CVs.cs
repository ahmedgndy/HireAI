using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireAI.Data.Models
{
    internal class CVs
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        [MaxLength(50)]
        public string Id { get; set; }

        [MaxLength(20)]
        public string? Phone { get; set; }

        [MaxLength(255)]
        public string? LinkedInPath { get; set; }

        [MaxLength(255)]
        public string? GitHubPath { get; set; }

        [MaxLength(100)]
        public string? Title { get; set; }

        [MaxLength(500)]
        public string? Education { get; set; }

        [MaxLength(1000)]
        public string? Experience { get; set; }

        public float? YearOfExperience { get; set; }

        [MaxLength(500)]
        public string? Certifications { get; set; }

        // Foreign Key
        public int? CandidatedId { get; set; }
    }
}
