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
    public class RefferFromService: IRefferFromService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<RefferFrom> _refferFromRepository;
        public RefferFromService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _refferFromRepository = _unitOfWork.Set<RefferFrom>();

        }

        public async Task<BaseResponseDto> GetAll()
        {
            var lstEducation = await _refferFromRepository.Select(g => Mapper.BaseMapper.Map(g)).ToListAsync();

            return new BaseResponseDto
            {
                Data = lstEducation,
                Status = ResponseStatus.Success
            };

        }
    }
}
