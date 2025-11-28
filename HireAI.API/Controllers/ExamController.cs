using HireAI.Service.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HireAI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly MockExamService _mockExamService;

        public ExamController(MockExamService mockExamService)
        {
            _mockExamService = mockExamService;
        }

        [HttpGet("QuickStats/{applicantId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMockExamQuickStatsAsync(int applicantId)
        {
            int MockExamsTakenNumber = await _mockExamService.GetMockExamsTakenNumberPerApplicantAsync(applicantId);
            int MockExamsTakenNumberForCurrentMonth = await _mockExamService.GetMockExamsTakenNumberForCurrentMonthPerApplicantAsync(applicantId);
            double AverageExamsTakenScore = await _mockExamService.GetAverageExamsTakenScorePerApplicantAsync(applicantId);
            double AverageExamsTakenScoreImprovement = await _mockExamService.GetAverageExamsTakenScoreImprovementPerApplicantAsync(applicantId);


            return Ok(new
            {
                MockExamsTakenNumber,
                MockExamsTakenNumberForCurrentMonth,
                AverageExamsTakenScore,
                AverageExamsTakenScoreImprovement
            });
        }

        [HttpGet("RecommendedMockExams/{applicantId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRecommendedMockExamsAsync(int applicantId)
        {
            var recommendedMockExams = await _mockExamService.GetRecommendedMockExamsPerApplicantAsync(applicantId);
            return Ok(recommendedMockExams);
        }

        [HttpGet("AllMockExams/{applicantId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllMockExamsAsync(int applicantId)
        {
            var allMockExams = await _mockExamService.GetAllMockExamsPerApplicantAsync(applicantId);
            return Ok(allMockExams);
        }
    }
}
