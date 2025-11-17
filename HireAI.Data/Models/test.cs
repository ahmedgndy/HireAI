using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireAI.Data.Models
{
    //just to upload this for the team 
    public class Test
    {

        public Guid Id { get; set; }


        public Guid CandidateId { get; set; } // Applicant Id
        public Guid ApplicationId { get; set; }//foreign key to Application
        public Application? Application { get; set; }
        public int NumberOfQuestions { get; set; }
        public int DurationInMinutes { get; set; }
        public DateTime CreatedAt { get; set; }
        public String TestName { get; set; } = null!;
        public bool IsAi { get; set; } = true;


        private Test() { }

    }
}