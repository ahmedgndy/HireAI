using HireAI.Data.Helpers.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireAI.Data.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string QuestionText { get; set; } = default!;
        public string[] Choices = new string[4];
        public int QuestionNumber { get; set; }
        public int CorrectAnswerIndex { get; set; }

        //Foreign Keys
        public int? ExamId { get; set; }
        public int? QuestionEvaluationId { get; set; }

        //Navigation Property
        public Exam? Exam { get; set; }
        public QuestionEvaluation? QuestionEvaluation { get; set; }
    }
}
