using AutoMapper;
using HireAI.Data.Helpers.DTOs.Exam;
using HireAI.Data.Helpers.Enums;
using HireAI.Data.Models;
using HireAI.Infrastructure.Context;
using HireAI.Infrastructure.Intrefaces;
using HireAI.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HireAI.Service.Implementation
{
    public class MockExamService : IMockExamService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IExamRepository _examRepository;
        private readonly IApplicantRepository _applicantRepository;
        private readonly HireAIDbContext _context;
        private readonly IMapper _mapper;

        public MockExamService(IApplicationRepository applicationRepository, IExamRepository examRepository,
             IApplicantRepository applicantRepository, HireAIDbContext context, IMapper mapper)
        {
            _applicationRepository = applicationRepository;
            _examRepository = examRepository;
            _applicantRepository = applicantRepository;
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> GetMockExamsTakenNumberPerApplicantAsync(int applicantId)
        {
            return await _examRepository.GetAll().CountAsync(e =>
            e.ApplicantId == applicantId && e.ExamType == enExamType.MockExam);
        }

        public async Task<int> GetMockExamsTakenNumberForCurrentMonthPerApplicantAsync(int applicantId)
        {
            var currentMonth = DateTime.UtcNow.Month;
            var currentYear = DateTime.UtcNow.Year;

            return await (from exam in _context.Set<Exam>()
                          join app in _context.Set<Application>() on exam.ApplicationId equals app.Id
                          where exam.ApplicantId == applicantId &&
                                exam.ExamType == enExamType.MockExam &&
                                app.DateApplied.Month == currentMonth &&
                                app.DateApplied.Year == currentYear
                          select exam).CountAsync();
        }

        public async Task<double> GetAverageExamsTakenScorePerApplicantAsync(int applicantId)
        {

            // Join ExamEvaluation -> ExamSummary -> Application and filter by Application.ApplicantId,
            // then compute the average TotalScore. Using joins avoids relying on navigation properties being loaded.
            var avg = await (from ev in _context.ExamEvaluations
                             join es in _context.Set<ExamSummary>() on ev.ExamSummaryId equals es.Id
                             join app in _context.Set<Application>() on es.ApplicationId equals app.Id
                             where app.ApplicantId == applicantId
                             select (double?)ev.TotalScore).AverageAsync();

            return avg ?? 0.0;
        }

        public async Task<double> GetAverageExamsTakenScoreImprovementPerApplicantAsync(int applicantId)
        {
            var applicationsWithScores = await (from app in _context.Set<Application>()
                                                where app.ApplicantId == applicantId
                                                select new
                                                {
                                                    ApplicationId = app.Id,
                                                    Scores = (from ev in _context.ExamEvaluations
                                                              join es in _context.Set<ExamSummary>() on ev.ExamSummaryId equals es.Id
                                                              where es.ApplicationId == app.Id
                                                              orderby es.CreatedAt
                                                              select ev.TotalScore).ToList()
                                                }).ToListAsync();

            var improvements = applicationsWithScores
                .Where(x => x.Scores.Count >= 2)
                .Select(x => x.Scores.Last() - x.Scores.First())
                .ToList();

            return improvements.Any() ? improvements.Average() : 0.0;
        }

        public async Task<IEnumerable<MockExamDto>> GetRecommendedMockExamsPerApplicantAsync(int applicantId)
        {
            var applicant = await _applicantRepository.GetAll()
                .AsNoTracking()
                .Include(a => a.ApplicantSkills)
                .ThenInclude(s => s.Skill)
                .FirstOrDefaultAsync(a => a.Id == applicantId);

            if (applicant == null)
            {
                throw new KeyNotFoundException("Applicant not found.");
            }

            // Get applicant's skill titles (normalized for comparison)
            var applicantSkillTitles = applicant.ApplicantSkills?
                .Where(s => s.Skill != null)
                .Select(s => s.Skill!.Name.ToLower().Trim())
                .ToHashSet() ?? new HashSet<string>();

            // Get all mock exams
            var allMockExams = await _examRepository.GetAll()
                .AsNoTracking()
                .Where(e => e.ExamType == enExamType.MockExam && e.ExamName != null)
                .ToListAsync();

            // Match exams where exam name contains any applicant skill
            var matchedExams = allMockExams
                .Where(e => applicantSkillTitles.Any(skill => e.ExamName.ToLower().Contains(skill)))
                .OrderBy(e => e.ExamLevel)
                .Take(6)
                .Select(e => _mapper.Map<MockExamDto>(e))
                .ToList();

            // If fewer than 3 matched exams, fill with additional exams by level
            if (matchedExams.Count < 3)
            {
                int skillCount = applicant.ApplicantSkills?.Count ?? 0;
                enExamLevel recommendedLevel = skillCount switch
                {
                    <= 2 => enExamLevel.Beginner,
                    <= 4 => enExamLevel.Intermediate,
                    <= 6 => enExamLevel.Advanced,
                    _ => enExamLevel.Beginner,
                };

                var additionalExams = allMockExams
                    .Where(e => e.ExamLevel == recommendedLevel &&
                                !matchedExams.Any(me => me.ExamName == e.ExamName))
                    .OrderBy(e => e.CreatedAt)
                    .Take(6 - matchedExams.Count)
                    .Select(e => _mapper.Map<MockExamDto>(e))
                    .ToList();

                matchedExams.AddRange(additionalExams);
            }

            return matchedExams;
        }

        public async Task<IEnumerable<MockExamDto>> GetAllMockExamsPerApplicantAsync(int applicantId)
        {
            var applicant = await _applicantRepository.GetAll()
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == applicantId);

            if (applicant == null)
            {
                throw new KeyNotFoundException("Applicant not found.");
            }

            // Get all mock exams and return 9 random ones
            var randomExams = await _examRepository.GetAll()
                .AsNoTracking()
                .Where(e => e.ExamType == enExamType.MockExam)
                .OrderBy(e => Guid.NewGuid())
                .Take(9)
                .Select(e => _mapper.Map<MockExamDto>(e))
                .ToListAsync();

            return randomExams;
        }
    }
}
