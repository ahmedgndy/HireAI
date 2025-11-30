using HireAI.Data.Helpers.DTOs.Applicant.response;
using HireAI.Data.Helpers.DTOs.ReportDtos.resposnes;
using HireAI.Infrastructure.GenericBase;
using HireAI.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireAI.Service.Services
{
    public class ReportService : IReportService
    {
        private readonly IJobPostRepository _jobPostRepository; 
        public ReportService(IJobPostRepository jobPostRepository) {
            _jobPostRepository = jobPostRepository;
        }
        public async Task<ReportDto> GetReportByJobIdAsync(int jobId)
        {
            var jobPost = await _jobPostRepository.GetByIdAsync(jobId); // fetch job once

            if (jobPost == null)
                throw new Exception("Job not found");

            var applicantDtos = await _jobPostRepository.GetApplicantDtosForJobAsync(jobId);

            var totalApplicants = jobPost.Applicants.Count;
            var atsPassingScore = jobPost.ATSMinimumScore ?? 0;
            var passedApplicants = jobPost.Applications.Count(a => a.AtsScore >= atsPassingScore);
            var atsPassPercent = totalApplicants == 0 ? 0 : (double)passedApplicants / totalApplicants * 100;

            var report = new ReportDto
            {
                JobTitle = jobPost.Title,
                TotalApplicants = totalApplicants,
                AtsPassPercent = atsPassPercent,
                AvgExamScore = 0, // add logic if needed
                Applicants = applicantDtos.ToList()
            };

            return report;
        }

    }
}

