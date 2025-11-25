using AutoMapper;
using HireAI.Data.Helpers.DTOs.ExamDTOS.Respones;
using HireAI.Infrastructure.GenericBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireAI.Service.Implementation
{
     public class ExamService
    {

        private readonly IExamRepository _examRepository;
        private readonly IMapper _mapper;
        public ExamService(IExamRepository examRepository ,IMapper mapper) { 
            _examRepository = examRepository; 
            _mapper = mapper;
        } 
        
        public async Task<ExamDTO?> GetExamByApplicantIdAsync(int applicantId)
        {
            var exam = await _examRepository.GetExamByApplicanIdAsync(applicantId);
            if (exam == null) return null;
            var examDTO = _mapper.Map<ExamDTO>(exam);

            return examDTO;
        }
    }
}
