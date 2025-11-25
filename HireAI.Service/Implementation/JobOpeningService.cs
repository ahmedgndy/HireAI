using AutoMapper;
using HireAI.Data.Helpers.DTOs.JopOpening.Request;
using HireAI.Data.Models;
using HireAI.Infrastructure.GenericBase;
using HireAI.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HireAI.Service.Implementation
{
    public class JobOpeningService : IJopOpenningService
    {
        private readonly IMapper _mapper;
        private readonly IJobOpeningRepository _jobOpeningRepository;

        public JobOpeningService(IMapper mapper , IJobOpeningRepository jobOpeningRepository)
        {
            _mapper = mapper;
            _jobOpeningRepository = jobOpeningRepository;
        }
       
 
        public async Task CreateJopOppenAsny(JopOpeingRequestDto jopOpeingRequestDto)
        {
            var createJobOpeningEntity = _mapper.Map<JobOpening>(jopOpeingRequestDto);
            await _jobOpeningRepository.AddAsync(createJobOpeningEntity);
        }

        public async Task DeleteJopOppenAsny(int id)
        {
            var jobOpeningEntity = await _jobOpeningRepository.GetByIdAsync(id);
            if (jobOpeningEntity == null)
            {
                throw new Exception("Job Opening not found");
            }
             await _jobOpeningRepository.DeleteAsync(jobOpeningEntity);
        }
    }

     

     
    }

