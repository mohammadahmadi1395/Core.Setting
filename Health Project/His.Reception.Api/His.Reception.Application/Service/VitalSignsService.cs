using His.Reception.Application.Interface;
using His.Reception.DTO;
using His.Reception.DTO.Message;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace His.Reception.Application.Service
{
    public class VitalSignsService: IVitalSignsService
    {
        public VitalSignsService()
        {

        }

        public Task<BaseResponseDto> AddAsync(VitalSignsDto patientDto)
        {
            throw new NotImplementedException();
        }
    }
}
