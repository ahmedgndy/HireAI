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
            var jobTitleTask = getJobTitle(jobId);
            var totalApplicantsTask = getTotalApplicants(jobId);
            var atsPassPercentTask = getAtsPassPercent(jobId);
            var applicantDtosTask = GetApplicantDtos(jobId);

             await Task.WhenAll(jobTitleTask, totalApplicantsTask, atsPassPercentTask, applicantDtosTask);

            var reportDto = new ReportDto
            {
                JobTitle = jobTitleTask.Result,
                TotalApplicants = totalApplicantsTask.Result,
                AtsPassPercent = atsPassPercentTask.Result,
                AvgExamScore = 0, // You may want to implement logic to calculate this
                Applicants = applicantDtosTask.Result.ToList()
            };

            return reportDto;
        }
        private async Task<string>  getJobTitle(int jobId)
        {
            var jobPost =await  _jobPostRepository.GetByIdAsync(jobId);
            return jobPost.Title;
        }

        private async Task<int> getTotalApplicants(int jobId)
        {
            var jobPost = await _jobPostRepository.GetByIdAsync(jobId);
            return jobPost.Applicants.Count;
        }

        private async Task<double> getAtsPassPercent(int jobId)
        {
            var jobPost = await _jobPostRepository.GetByIdAsync(jobId);
            var totalApplicants = jobPost.Applicants.Count;

            if (totalApplicants == 0) return 0;
            var atsPassingScore = jobPost.ATSMinimumScore ?? 0;

            var passedApplicants = jobPost.Applications.Count(a => a.AtsScore >= atsPassingScore);
            return (double)passedApplicants / totalApplicants * 100;
        }

        private async Task<ICollection<ApplicantDto>> GetApplicantDtos(int jobId)
        {

            var applicantDtos =  await _jobPostRepository.GetApplicantDtosForJobAsync(jobId);
            return applicantDtos;
        }
    }
}

