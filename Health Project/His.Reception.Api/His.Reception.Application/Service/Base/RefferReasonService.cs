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
    public class RefferReasonService: IRefferReasonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<RefferReason> _refferReasonRepository;
        public RefferReasonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _refferReasonRepository = _unitOfWork.Set<RefferReason>();

        }

        public async Task<BaseResponseDto> GetAll()
        {
            var lstService = await _refferReasonRepository.Select(g => Mapper.BaseMapper.Map(g)).ToListAsync();

            return new BaseResponseDto
            {
                Data = lstService,
                Status = ResponseStatus.Success
            };
        }
    }
}
