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
    public class BirthPlaceService: IBirthPlaceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<BirthPlace> _birthPlaceRepository;
        public BirthPlaceService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _birthPlaceRepository = _unitOfWork.Set<BirthPlace>();

        }

        public async Task<BaseResponseDto> GetAll()
        {
            var lstEducation = await _birthPlaceRepository.Select(g =>Mapper.BaseMapper.Map(g)).ToListAsync();

            return new BaseResponseDto
            {
                Data = lstEducation,
                Status = ResponseStatus.Success
            };

        }
    }
}
