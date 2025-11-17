using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace HireAI.Data.Models
{
    public class TestAttempt
    {

       
        public Guid Id { get; set; }

        public Guid JobId { get; set; }
        public Job Job { get; set; } = null!;

        public Guid ApplicationId { get; set; }
        public Application Application { get; set; } = null!;

        public Guid TestId { get; set; }
        public Test Test { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

      
        public ICollection<CandidateResponse>? CandidateResponses { get; set; }
        public TestEvaluation? TestEvaluation { get; set; }
    

    }
}
