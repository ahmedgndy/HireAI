using HireAI.Data.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireAI.Data.Models
{
   
        public class User
        {
          
            public Guid Id { get; set; }

      
            public string Name { get; set; }

          
            public string Email { get; set; } = null!;

        
            public string PasswordHash { get; set; } = null!;

      
            public Role Role { get; set; }

        public string? phone { get; set; }
        public string? Bio { get; set; }
        public string? Title { get; set; }
        public DateTime? LastLogin { get; set; }

     
            public DateTime CreatedAt { get; set; }
        }
    }
}

