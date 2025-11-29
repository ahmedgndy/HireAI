using HireAI.Data.Helpers.Enums;


namespace HireAI.Data.Helpers.DTOs.ApplicantApplication
{
    public class ApplicationDetailsDto
    {
        public string JobTitle { get; set; } = default!;
        public string CompanyName { get; set; } = default!;
        public string CompanyLocation { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int NumberOfApplicants { get; set; }
        public float? AtsScore { get; set; }
        public enApplicationStatus ApplicationStatus { get; set; }
        public bool IsPassed { get; set; }
        public enExamEvaluationStatus ExamEvaluationStatus { get; set; } = enExamEvaluationStatus.Pending;

    }
}
