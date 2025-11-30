using HireAI.Data.Helpers.DTOs.Applicant.response;
using HireAI.Data.Helpers.DTOs.ReportDtos.resposnes;
using HireAI.Data.Models;
using HireAI.Infrastructure.Context;
using HireAI.Infrastructure.GenaricBasies;
using HireAI.Infrastructure.GenericBase;
using HireAI.Infrastructure.Intrefaces;
using Microsoft.EntityFrameworkCore;

namespace HireAI.Infrastructure.Repositories
{
    public class JobPostRepository : GenericRepositoryAsync<JobPost>, IJobPostRepository
    {
        public JobPostRepository(HireAIDbContext db) : base(db) { }

        public async Task<ICollection<JobPost>?> GetJobPostForHrAsync(int hrid)
        {
            return await _dbSet
                           .Where(jo => jo.HRId == hrid)
                           .Include(jp => jp.JobSkills)
                           .ThenInclude(js => js.Skill)
                           .ToListAsync();
        }

        public override Task<JobPost?> GetByIdAsync(int id)
        {
            return _dbSet
                    .Include(jp => jp.JobSkills)
                    .ThenInclude(js => js.Skill)
                    .FirstOrDefaultAsync(jp => jp.Id == id);

        }
        public async Task<ICollection<ApplicantDto>> GetApplicantDtosForJobAsync(int jobId)
        {
            var jobPost = await _context.JobPosts
                .Include(j => j.Applications)
                    .ThenInclude(a => a.Applicant)
                .Include(j => j.Applications)
                .Include(a => a.ExamEvaluations)
                .FirstOrDefaultAsync(j => j.Id == jobId);

            if (jobPost == null) return new List<ApplicantDto>();

            var applicantDtos = jobPost.Applications
                .Select(a => new ApplicantDto
                {
                    Name = $"{a.Applicant.Name}",
                    Email = a.Applicant.Email,
                    AtsScore = a.AtsScore ?? 1.0f,
                    ExamScore = a != null ? a.ExamEvaluation.TotalScore : (float?)null,
                    Status = a.ExamStatus.ToString()
                })
                .ToList();

            return applicantDtos;
        }


    }
}
