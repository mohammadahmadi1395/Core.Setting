using His.Reception.Application.Interface.Base;
using His.Reception.DAL.Context;
using His.Reception.DTO.Message;
using His.Reception.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace His.Reception.Application.Service.Base
{
    public class MaritalStatusService: IMaritalStatusService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<MaritalStatus> _maritalStatusRepository;
        public MaritalStatusService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _maritalStatusRepository = _unitOfWork.Set<MaritalStatus>();

        }

        public async Task<BaseResponseDto> GetAll()
        {
            var lstEducation = await _maritalStatusRepository.Select(g => Mapper.BaseMapper.Map(g)).ToListAsync();

            return new BaseResponseDto
            {
                Data = lstEducation,
                Status = ResponseStatus.Success
            };

        }
    }
}
