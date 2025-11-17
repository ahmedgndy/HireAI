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
        public Guid ApplicationId { get; set; }//foreign key to Application
        public int NumberOfQuestions { get; set; }
        public int DurationInMinutes { get; set; }
        public DateTime CreatedAt { get; set; }
        public String TestName { get; set; } =  null!;
        public bool IsAi { get; set; } = true;


        private Test() { } 


        public Test(string testName, int numberOfQuestions, int durationInMinutes, Guid applicationId, bool isAi = true)
        {
            TestName = testName;
            NumberOfQuestions = numberOfQuestions;
            DurationInMinutes = durationInMinutes;
            ApplicationId = applicationId;
            IsAi = isAi;

            Validitor();
        }

        //self validation
        private void Validitor() { 
            if (NumberOfQuestions < 300)
            {
                throw new ArgumentException("Number of questions must be at least 10");
            }

            if(NumberOfQuestions >=0)
            {
                
               throw new ArgumentException("Number of questions must be bigger than 10 ");
                
            }
            if (DurationInMinutes <= 0)
            {
                throw new ArgumentException("Duration must be greater than 0");
            }
        }
    }
}
