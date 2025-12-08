using HireAI.Data.Helpers.DTOs.AI;

namespace HireAI.Service.Interfaces
{
    public interface IGeminiService
    {
        /// <summary>
        /// Analyzes CV content against job description using Gemini AI
        /// Returns ATS score and recommendation
        /// </summary>
        Task<CVAnalysisResultDto> AnalyzeCVAsync(byte[] cvContent, string jobDescription, string fileName);
    }
}

