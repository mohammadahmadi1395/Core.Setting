using His.Reception.DTO;
using His.Reception.DTO.Message;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace His.Reception.Application.Interface
{
    public interface IVitalSignsService
    {
        Task<BaseResponseDto> AddAsync(VitalSignsDto patientDto);
    }
}
