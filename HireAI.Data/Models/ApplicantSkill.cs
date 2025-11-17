using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireAI.Data.Models
{
    public   class ApplicantSkill
    {

        public Guid Id { get; set; }

        public Guid CandidateId { get; set; } // Applicant Id
        public Applicant Candidate { get; set; } = null!;

        public Guid SkillId { get; set; }
      
        public Skill Skill { get; set; } = null!;

        public float? SkillRate { get; set; }
    }
}
