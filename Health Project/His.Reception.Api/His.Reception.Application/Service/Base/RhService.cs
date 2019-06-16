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
    public class RhService: IRhService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<Rh> _rhRepository;
        public RhService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _rhRepository = _unitOfWork.Set<Rh>();

        }

        public async Task<BaseResponseDto> GetAll()
        {
            var lstService = await _rhRepository.Select(g => Mapper.BaseMapper.Map(g)).ToListAsync();

            return new BaseResponseDto
            {
                Data = lstService,
                Status = ResponseStatus.Success
            };
        }
    }
}
