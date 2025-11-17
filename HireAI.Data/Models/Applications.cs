using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireAI.Data.Models
{
    public class Applications
    {
        public Guid Id { get; set; }
        public Guid JobId { get; set; }
        public int Status { get; set; }
        public DateTime DateApplied { get; set; }
        public string CVFilePath { get; set; }
        public float ScoreATs { get; set; }
        public Guid CandidateId { get; set; }
    }
}
