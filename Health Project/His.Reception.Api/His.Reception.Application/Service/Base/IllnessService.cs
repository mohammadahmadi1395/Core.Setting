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
    public class IllnessService : IIllnessService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<Illness> _illnessesRepository;
        public IllnessService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _illnessesRepository = _unitOfWork.Set<Illness>();
        }

        public async Task<BaseResponseDto> GetAll()
        {
            var lstService = await _illnessesRepository.Select(g => Mapper.IllnessMapper.Map(g)).ToListAsync();

            return new BaseResponseDto
            {
                Data = lstService,
                Status = ResponseStatus.Success
            };
        }
    }
}
