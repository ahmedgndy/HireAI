using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireAI.Data.Models
{
    public class ApplicantSkill
    {
        int Id { get; set; }
        int CandidateId { get; set; }
        int SkillId { get; set; }
        float SkillRate { get; set; }
    }
}
