using His.Reception.DTO;
using His.Reception.DTO.Message;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace His.Reception.Application.Interface
{
    public interface IReceptionsService
    {
        Task<BaseResponseDto> AddAsync(ReceptionDto receptionDto);
        Task<BaseResponseDto> EditAsync(ReceptionDto receptionDto);
        Task<BaseResponseDto> MaxReceptionId();
        Task<BaseResponseDto> GetListReception(FilterReceptionDto filterReceptionDto);

    }
}
