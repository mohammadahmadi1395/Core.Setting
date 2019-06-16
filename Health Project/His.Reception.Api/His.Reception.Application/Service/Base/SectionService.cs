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
    public class SectionService : ISectionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<Section> _sectionRepository;
        public SectionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _sectionRepository = _unitOfWork.Set<Section>();

        }

        public async Task<BaseResponseDto> GetAll()
        {
            var lstService = await _sectionRepository.Select(g => Mapper.SectionMapper.Map(g)).ToListAsync();

            return new BaseResponseDto
            {
                Data = lstService,
                Status = ResponseStatus.Success
            };
        }
    }
}
