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
            // Eager load Applicants and Applications
            var job = await _context.JobPosts
                .Include(j => j.Applicants)
                .Include(j => j.Applications)
                .ThenInclude(g=>g.ExamEvaluation)
                .AsNoTracking() // for read-only queries
                .FirstOrDefaultAsync(j => j.Id == jobId);

            if (job == null)
                return new List<ApplicantDto>();

            // Map applicants to DTOs
            var applicantDtos = job.Applicants.Select(a =>
            {
                var app = job.Applications.FirstOrDefault(ap => ap.ApplicantId == a.Id);
                return new ApplicantDto
                {
                   
                    Name = a.Name,
                    Email = a.Email,
                    AtsScore = app?.AtsScore ?? 0,
                    ExamScore = app?.ExamEvaluation.TotalScore,
              
                };
            }).ToList();

            return applicantDtos;
        }


    }
}
