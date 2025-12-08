//using HireAI.Data.Models;
//using HireAI.Infrastructure.Context;
//using HireAI.Infrastructure.GenaricBasies;
//using HireAI.Infrastructure.GenericBase;
//using HireAI.Infrastructure.Intrefaces;
//using Microsoft.EntityFrameworkCore;

//namespace HireAI.Infrastructure.Repositories
//{
//    public class JobPostRepository : GenericRepositoryAsync<JobPost>, IJobPostRepository
//    {
//        public JobPostRepository(HireAIDbContext db) : base(db) { }

//        public async Task<ICollection<JobPost>?> GetJobPostForHrAsync(int hrid)
//        {
//            return await _dbSet
//                           .Where(jo => jo.HRId == hrid)
//                           .Include(jp => jp.JobSkills)
//                           .ThenInclude(js => js.Skill)
//                           .ToListAsync();
//        }

//        public override Task<JobPost?> GetByIdAsync(int id)
//        {
//            return _dbSet
//                    .Include(jp => jp.JobSkills)
//                    .ThenInclude(js => js.Skill)
//                    .FirstOrDefaultAsync(jp => jp.Id == id);

//        }

//        public async Task<int> GetTotalApplicationsByJobIdAsync(int jobId)
//        {
//            return await _context.Applications
//                .Where(app => app.JobId == jobId)
//                .CountAsync();
//        }


//    }
//}
