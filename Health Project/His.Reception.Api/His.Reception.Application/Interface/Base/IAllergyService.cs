using His.Reception.DTO.Message;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace His.Reception.Application.Interface.Base
{
    public interface IAllergyService
    {
        Task<BaseResponseDto> GetAll();
    }
}
