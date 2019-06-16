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
    public class AllergyService : IAllergyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<Allergy> _allergyRepository;
        public AllergyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _allergyRepository = _unitOfWork.Set<Allergy>();

        }

        public async Task<BaseResponseDto> GetAll()
        {
            var lstEducation = await _allergyRepository.Select(g => Mapper.BaseMapper.Map(g)).ToListAsync();

            return new BaseResponseDto
            {
                Data = lstEducation,
                Status = ResponseStatus.Success
            };

        }
    }
}
