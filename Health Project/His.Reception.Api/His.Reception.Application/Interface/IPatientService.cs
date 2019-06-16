using His.Reception.DTO;
using His.Reception.DTO.Message;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace His.Reception.Application.Interface
{
    public interface IPatientService
    {
        Task<BaseResponseDto> GetPatientByID(long personId);
        Task<PageListResponse> GetListPatient(FilterPatientDto filterPatientDto);

        Task<BaseResponseDto> AddAsync(PatientDto patientDto);
        Task<BaseResponseDto> EditAsync(PatientDto patientDto);
        Task<BaseResponseDto> GetPatientBaseInfo();
        Task<BaseResponseDto> FindPatient(SearchPatientDto searchPatientDto);
        Task<BaseResponseDto> MaxInternalId();


    }
}
