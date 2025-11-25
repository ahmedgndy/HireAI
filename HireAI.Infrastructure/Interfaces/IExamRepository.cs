using HireAI.Data.Models;
using HireAI.Infrastructure.GenaricBasies;

namespace HireAI.Infrastructure.GenericBase
{
    public interface IExamRepository : IGenericRepositoryAsync<Exam> {
        public Task<Exam?> GetExamByApplicanIdAsync(int applicantId);
    }
}
