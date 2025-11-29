using HireAI.Service.Implementation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HireAI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantController : ControllerBase
    {
        private readonly ApplicantDashboardService _applicantDashboardService;
        private readonly ApplicantApplicationService _applicantApplicationService;

        public ApplicantController(ApplicantDashboardService applicantDashboardService, ApplicantApplicationService applicantApplicationService)
        {
            _applicantDashboardService = applicantDashboardService;
            _applicantApplicationService = applicantApplicationService;
        }

        [HttpGet("Dashboard/{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetApplicantDashboardAsync(int Id)
        {
            int ActiveApplicationsNum = await _applicantDashboardService.GetActiveApplicationsNumberPerApplicantAsync(Id);
            int MockExamsTakenNumber = await _applicantDashboardService.GetMockExamsTakenNumberPerApplicantAsync(Id);
            double AverageExamsTakenScore = await _applicantDashboardService.GetAverageExamsTakenScorePerApplicantAsync(Id);
            string SkillLevel = await _applicantDashboardService.GetSkillLevelPerApplicantAsync(Id);
            var ApplicationTimeline = await _applicantDashboardService.GetApplicationTimelinePerApplicantAsync(Id);
            var ApplicantSkillImprovementScore = await _applicantDashboardService.GetApplicantSkillImprovementScoreAsync(Id);

            return Ok(new
            {
                ActiveApplicationsNum,
                MockExamsTakenNumber,
                AverageExamsTakenScore,
                SkillLevel,
                ApplicationTimeline,
                ApplicantSkillImprovementScore
            });
        }

        [HttpGet("ApplicationsList/{applicantId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetApplicationsListAsync(int applicantId)
        {
            var applicationsList = await _applicantApplicationService.GetApplicantApplicationsList(applicantId);
            return Ok(applicationsList);
        }

        [HttpGet("ApplicationDetails/{applicationId:int},{applicantId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetApplicationDetailsAsync(int applicationId, int applicantId)
        {
            var applicationsDetails = await _applicantApplicationService.GetApplicationDetailsAsync(applicationId, applicantId);
            return Ok(applicationsDetails);
        }
    }
}
