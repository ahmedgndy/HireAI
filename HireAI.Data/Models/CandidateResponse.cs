using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireAI.Data.Models
{
    public  class CandidateResponse
    {
        public Guid Id { get; set; }

        public Guid QuestionId { get; set; }
    
        public Question Question { get; set; } = null!;

        public Guid TestAttemptId { get; set; }
     
        public TestAttempt TestAttempt { get; set; } = null!;

        public int AnswerNumber { get; set; }

        // Navigation
        public QuestionEvaluation? QuestionEvaluation { get; set; }
    }
}
