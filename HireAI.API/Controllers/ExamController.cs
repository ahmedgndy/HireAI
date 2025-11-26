using HireAI.Data.Helpers.DTOs.ExamDTOS.Request;
using HireAI.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HireAI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IExamService _examService;
        public ExamController(IExamService examService) { 
            _examService = examService;
        }


        
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetExamByApplicantId(int id)
        {

           Console.WriteLine("Received request for applicant ID: " + id);
            var examDTO =  await _examService.GetExamByApplicantIdAsync(id);
          return Ok(examDTO);

        }
        [HttpGet("taken/{applicantId:int}")]
        public async Task<IActionResult> GetExamsTakenByApplicant(int applicantId, int pageNumber = 1, int pageSize = 5)
        {
            var examsDTO = await _examService.GetExamsTakenByApplicant(applicantId, pageNumber, pageSize);
            return Ok(examsDTO);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExam([FromBody] ExamRequestDTO examRequestDTO)
        {
            await _examService.CreateExamAsync(examRequestDTO);
            return Ok();
        }

        [HttpPost("question")]
        public async Task<IActionResult> CreateQuestion([FromBody] QuestionRequestDTO questionRequestDTO)
        {
            await _examService.CreateQuestionAsync(questionRequestDTO);
            return Ok();
        }
        [HttpDelete("{examId:int}")]
        public async Task<IActionResult> DeleteExam(int examId)
        {
            await _examService.DeleteExamAsync(examId);
            return Ok();
        }
    }
}
