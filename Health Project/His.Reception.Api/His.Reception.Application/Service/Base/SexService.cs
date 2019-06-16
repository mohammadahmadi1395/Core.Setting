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
    public class SexService: ISexService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<Sex> _sexRepository;
        public SexService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _sexRepository = _unitOfWork.Set<Sex>();

        }

        public async Task<BaseResponseDto> GetAll()
        {
            var lstService = await _sexRepository.Select(g => Mapper.BaseMapper.Map(g)).ToListAsync();

            return new BaseResponseDto
            {
                Data = lstService,
                Status = ResponseStatus.Success
            };
        }
    }
}
