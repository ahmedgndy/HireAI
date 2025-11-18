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
        public int Id { get; set; }
        public int SkillId { get; set; }
        public Skill Skill { get; set; } = null!;
        public float? SkillRate { get; set; }

        //navigations 
        public int ApplicantId { get; set; } //fk
        public Applicant Applicant { get; set; }


    }
}
