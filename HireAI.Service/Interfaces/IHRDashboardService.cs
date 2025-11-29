using HireAI.Data.Helpers.DTOs.Respones;
using HireAI.Data.Helpers.DTOs.Respones.HRDashboardDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireAI.Service.Interfaces
{
    public interface IHRDashboardService
    {
        public Task<HRDashboardDto> GetDashboardAsync(int hrId);

        // Individual dashboard metric endpoints
        public Task<int> GetTotalApplicantsAsync(int hrId);
        public Task<int> GetTotalExamTakenAsync(int hrId);
        public Task<int> GetTotalTopCandidatesAsync(int hrId);
        public Task<Dictionary<int, int>> GetMonthlyNumberOfApplicationsAsync(int hrId);
        public Task<Dictionary<int, int>> GetMonthlyOfTotalATSPassedAsync(int hrId);
        public Task<List<RecentApplicationDto>> GetRecentApplicantsAsync(int hrId, int take = 5);
        public Task<List<ActiveJopPosting>> GetActiveJobPostingsAsync(int hrId);
    }
}
