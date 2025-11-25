using System;
using System.Collections.Generic;
using HireAI.Data.Helpers.Enums;

namespace HireAI.Data.Helpers.DTOs.ExamDTOS.Respones
{
    public class ExamDTO
    {
        public int Id { get; set; }
        public int NumberOfQuestions { get; set; }
        public int DurationInMinutes { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ExamName { get; set; } = default!;
        public bool IsAi { get; set; } = true;
        public enExamType ExamType { get; set; } = enExamType.MockExam;

        public int? ApplicantId { get; set; }
        public int? ApplicationId { get; set; }

        public List<QuestionDTO> Questions { get; set; } = new List<QuestionDTO>();
    }

  

}
