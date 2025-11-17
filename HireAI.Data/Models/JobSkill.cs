using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireAI.Data.Models
{
    public  class JobSkill
    {
        public Guid Id { get; set; }

        public Guid JobId { get; set; }
        public Job Job { get; set; } = null!;

        public Guid SkillId { get; set; }

        public Skill Skill { get; set; } = null!;
    }
}
