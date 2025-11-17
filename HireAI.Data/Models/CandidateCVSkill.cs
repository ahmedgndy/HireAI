using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireAI.Data.Models
{
    public class CandidateCVSkill
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }

        public Guid CVId { get; set; }
        
    }
}
