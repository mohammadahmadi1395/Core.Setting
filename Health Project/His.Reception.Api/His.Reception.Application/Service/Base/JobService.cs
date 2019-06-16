using His.Reception.Application.Interface.Base;
using His.Reception.Application.Validation;
using His.Reception.DAL.Context;
using His.Reception.DTO;
using His.Reception.DTO.Message;
using His.Reception.Entities.Models;
using His.Reception.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace His.Reception.Application.Service.Base
{
    public class JobService:IJobService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<Job> _jobRepository;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        public JobService(IUnitOfWork unitOfWork, IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _unitOfWork = unitOfWork;
            _jobRepository = _unitOfWork.Set<Job>();
            _sharedLocalizer = sharedLocalizer;
        }

        public async Task<BaseResponseDto> GetAll()
        {
            var lstEducation = await _jobRepository.Select(g => Mapper.BaseMapper.Map(g)).ToListAsync();

            return new BaseResponseDto
            {
                Data = lstEducation,
                Status = ResponseStatus.Success
            };
        }

        public async Task<BaseResponseDto> AddAsync(BaseDto baseDto)
        {
            var resultVaild = CheckValidate.Vaild<BaseDto>(new BaseValidation(_sharedLocalizer), baseDto);

            if (resultVaild.Status == ResponseStatus.Fail)
            {
                return resultVaild;
            }

            //var curJob =await _jobRepository.Where(j => j.Title == baseDto.Title).FirstOrDefaultAsync();

            //if(curJob != null) {
            //     return new BaseResponseDto
            //     {
            //         Status = ResponseStatus.Fail,
            //         Message=

            //     };
            // }

            var mapJob=(Job)Mapper.BaseMapper.Map(new Job(),baseDto);
            await _jobRepository.AddAsync(mapJob);

            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data=mapJob.Id
            };
        }
             
    }
}
