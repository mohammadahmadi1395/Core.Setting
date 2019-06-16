using His.Reception.DTO;
using His.Reception.DTO.Message;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace His.Reception.Application.Interface
{
    public interface IDoctorService
    {
        Task<BaseResponseDto> GetDoctoryById(int id);
        Task<BaseResponseDto> GetAll();
        Task<BaseResponseDto> AddAsync(DoctorDto  doctorDto);
        Task<BaseResponseDto> EditAsync(DoctorDto doctorDto);

    }
}
