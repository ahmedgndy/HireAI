using HireAI.Data.Helpers.DTOs.Application;
using HireAI.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HireAI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllAsync()
        {
            var applications = await _applicationService.GetAllApplicationsAsync();
            return Ok(applications);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var application = await _applicationService.GetApplicationByIdAsync(id);
            
            if (application == null)
                return NotFound(new { message = $"Application with ID {id} not found" });

            return Ok(application);
        }

        [HttpGet("applicant/{applicantId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "Applicant")]
        public async Task<IActionResult> GetByApplicantIdAsync(int applicantId)
        {
            var applications = await _applicationService.GetApplicationsByApplicantIdAsync(applicantId);
            return Ok(applications);
        }

        [HttpGet("job/{jobId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByJobIdAsync(int jobId)
        {
            var applications = await _applicationService.GetApplicationsByJobIdAsync(jobId);
            return Ok(applications);
        }

        [HttpGet("hr/{hrId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize(Roles = "HR")]
        public async Task<IActionResult> GetByHRIdAsync(int hrId)
        {
            var applications = await _applicationService.GetApplicationsByHRIdAsync(hrId);
            return Ok(applications);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Applicant")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateApplicationDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var application = await _applicationService.CreateApplicationAsync(createDto);
                return Ok(application);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize(Roles = "Applicant")]
        public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateApplicationDto updateDto)
        {
            if (id != updateDto.Id)
                return BadRequest(new { message = "ID mismatch" });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var application = await _applicationService.UpdateApplicationAsync(updateDto);
                return Ok(application);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Authorize(Roles = "HR,Applicant")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _applicationService.DeleteApplicationAsync(id);
            
            if (!result)
                return NotFound(new { message = $"Application with ID {id} not found" });

            return NoContent();
        }
    }
}