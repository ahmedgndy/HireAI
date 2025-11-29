using AutoMapper;
using HireAI.Data.Helpers.DTOs.JopOpening.Request;
using HireAI.Data.Helpers.DTOs.JopOpeningDtos.Response.HireAI.Data.Helpers.DTOs.JopOpeningDtos.Response;
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

    public class JopPostService : IJobPostService

    {
        private readonly IMapper _mapper;
        private readonly IJobPostRepository _jobPostRepository;
        private readonly IJobSkillRepository _jobSkillRepository;

        public JopPostService(IMapper mapper , IJobPostRepository jobOpeningRepository , IJobSkillRepository jobSkillRepository)
        {
            _mapper = mapper;
            _jobPostRepository = jobOpeningRepository;
            _jobSkillRepository = jobSkillRepository;
        }

        public async Task CreateJobPostAsync(JobPostRequestDto jopOpeingRequestDto)
        {
            //save job 
            var createPostEntity = _mapper.Map<JobPost>(jopOpeingRequestDto);

            await _jobPostRepository.AddAsync(createPostEntity);

            //tie skills to job
            var skillIds = jopOpeingRequestDto.SkillIds;
            if (skillIds != null && skillIds.Any())
            {
              for (int i = 0; i < skillIds.Count(); i++)
                {
                    var jobSkillEntity = new JobSkill
                    {
                        JobId = createPostEntity.Id,
                        SkillId = skillIds.ElementAt(i)
                    };
                    await _jobSkillRepository.AddAsync(jobSkillEntity); ;
                }   
            }
        }

  
        public async Task DeleteJobPostAsync(int id)
        {
            var jobOpeningEntity = await _jobPostRepository.GetByIdAsync(id);
            if (jobOpeningEntity == null)
            {
                throw new Exception("Job Opening not found");
            }
            await _jobPostRepository.DeleteAsync(jobOpeningEntity);
        }

        public async Task<JobPostResponseDto> GetJobPostAsync(int id)
        {
           var jopPost = await _jobPostRepository.GetByIdAsync(id);
           
           if(jopPost == null)
           {
                  throw new Exception("Job Post not found");
           }

           var jobSkills = await _jobSkillRepository.GetSkillsByJobIdAsync(id);
            jopPost.JobSkills = jobSkills.ToList();
            return _mapper.Map<JobPostResponseDto>(jopPost);
        }

        public async Task<ICollection<JobPostResponseDto>> GetJobPostForHrAsync(int hrid)
        {
            var jobOpenings = await _jobPostRepository.GetJobPostForHrAsync(hrid);

            if (jobOpenings == null || !jobOpenings.Any())
            {
                return Array.Empty<JobPostResponseDto>();
            }
            return _mapper.Map<ICollection<JobPostResponseDto>>(jobOpenings);
        }

        public async Task UpdateJobPostAsync(int id, JobPostRequestDto jopOpeingRequestDto)
        {
            var existingEntity = await _jobPostRepository.GetByIdAsync(id);

            if (existingEntity == null)
                throw new Exception("Job Opening not found");


            _mapper.Map(jopOpeingRequestDto, existingEntity);

            await _jobPostRepository.UpdateAsync(existingEntity);
        }
    }
}
      
 
       
     



