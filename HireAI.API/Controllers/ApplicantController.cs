using HireAI.Data.Helpers.DTOs.Applicant.Request;
using HireAI.Data.Models;
using HireAI.Infrastructure.GenaricBasies;
using HireAI.Service.Implementation;
using HireAI.Service.Interfaces;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace HireAI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicantController : ControllerBase
    {
        private readonly ApplicantDashboardService _applicantDashboardService;
        private readonly ApplicantApplicationService _applicantApplicationService;
        private readonly IS3Service _s3Service;

        public ApplicantController(ApplicantDashboardService applicantDashboardService, ApplicantApplicationService applicantApplicationService , IS3Service s3Service )
        {
            _applicantDashboardService = applicantDashboardService;
            _applicantApplicationService = applicantApplicationService;
            _s3Service = s3Service;
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

        [HttpPost]
        [RequestSizeLimit(10 * 1024 * 1024)]
        [Route("UploadResume/{applicantId:int}")]
        public async Task<IActionResult> UploadResumeAsync([FromForm] ApplicantCreateDto dto)
        {
            if (dto == null || dto.CvFile == null)
            {
                return BadRequest("No file uploaded.");
            }

            // Upload to S3
            string resumeUrl;
            try
            {
                // Cast dto.CvFile to Microsoft.AspNetCore.Http.IFormFile f possible
                resumeUrl = await _s3Service.UploadFileAsync(dto.CvFile );
            }
            catch (Exception ex)
            {
                // log ex in real app
                return StatusCode(500, $"Failed to upload file: {ex.Message}");
            }
            Console.WriteLine($"Resume uploaded successfully. URL: {resumeUrl}");

            // The rest of your logic here...
            // return Ok(resumeUrl); // or whatever is appropriate
            return Ok(resumeUrl);
        }
    }
}
