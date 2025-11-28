using AutoMapper;
using HireAI.Infrastructure.Intrefaces;
using HireAI.Data.DTOs.ApplicantDashboard;
using Microsoft.EntityFrameworkCore;
using HireAI.Data.Helpers.DTOs.ApplicantApplication;
using HireAI.Service.Interfaces;

namespace HireAI.Service.Implementation
{
    public class ApplicantApplicationService : IApplicantApplicationService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IMapper _mapper;

        public ApplicantApplicationService(IApplicationRepository applicationRepository, IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ApplicantApplicationsListDto>> GetApplicantApplicationsList(int applicantId)
        {
            var ApplicationsList = await _applicationRepository.GetAll()
                .AsNoTracking()
                .Include(a => a.AppliedJob)
                .Where(a => a.ApplicantId == applicantId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ApplicantApplicationsListDto>>(ApplicationsList);
        }

        public async Task<ApplicationDetailsDto> GetApplicationDetailsAsync(int applicationId, int applicantId)
        {
            var application = await _applicationRepository.GetAll()
                .AsNoTracking()
                .Include(a => a.AppliedJob)
                .Include(a => a.ExamSummary)
                .ThenInclude(e => e.ExamEvaluation)
                .Where(a => a.Id == applicationId && a.ApplicantId == applicantId)
                .FirstOrDefaultAsync();

            if (application == null)
            {
                throw new KeyNotFoundException("Application not found.");
            }

            return _mapper.Map<ApplicationDetailsDto>(application);
        }
    }
}
