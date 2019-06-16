using His.Reception.Application.Interface;
using His.Reception.Application.Interface.Base;
using His.Reception.Application.Validation;
using His.Reception.DAL.Context;
using His.Reception.DAL.Extensions;
using His.Reception.DTO;
using His.Reception.DTO.Message;
using His.Reception.Entities.Models;
using His.Reception.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace His.Reception.Application.Service
{
    public class PatientService:IPatientService
    {
        #region  dependency

        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<Person> _personRepository;
        private readonly DbSet<Patient> _patientRepository;

        private readonly IDoctorService _doctorService;
        private readonly IBirthPlaceService _birthPlaceService;
        private readonly IBloodGroupService _bloodGroupService;
        private readonly IEducationService _educationService;
        private readonly IIllnessService _illnessService;
        private readonly IGeneralStatusService _generalStatusService;
        private readonly IIssuePlaceService _issuePlaceService;
        private readonly IJobService _jobService;
        private readonly IMaritalStatusService  _maritalStatusService;
        private readonly IReceptionTypeService  _receptionTypeService;
        private readonly IRefferReasonService  _refferReasonService;
        private readonly IRegionalService  _regionalService;
        private readonly IRhService  _rhService;
        private readonly ISectionService  _sectionService;
        private readonly ISexService  _sexService;
        private readonly ISpecialIllnessService _specialIllnessService;
        private readonly IAllergyService _allergyService;
        private readonly IPresenterService _presenterService;
        private readonly IRefferFromService _refferFromService;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;

        #endregion

        public PatientService(
            IUnitOfWork unitOfWork,
            IDoctorService doctorService,
            IBirthPlaceService birthPlaceService,
            IBloodGroupService bloodGroupService,
            IEducationService educationService,
            IIllnessService illnessService,
            IGeneralStatusService generalStatusService,
            IIssuePlaceService issuePlaceService,
            IJobService jobService,
            IMaritalStatusService maritalStatusService,
            IReceptionTypeService receptionTypeService,
            IRefferReasonService refferReasonService,
            IRegionalService regionalService,
            IRhService rhService,
            ISectionService sectionService,
            ISexService sexService,
            ISpecialIllnessService specialIllnessService,
            IAllergyService allergyService,
            IPresenterService presenterService,
            IRefferFromService refferFromService,
             IStringLocalizer<SharedResource> sharedLocalizer
            )
        {
            _unitOfWork = unitOfWork;
            _personRepository = _unitOfWork.Set<Person>();
            _patientRepository = _unitOfWork.Set<Patient>();
            _doctorService = doctorService;
            _birthPlaceService = birthPlaceService;
            _bloodGroupService = bloodGroupService;
            _educationService = educationService;
            _illnessService = illnessService;
            _generalStatusService = generalStatusService;
            _issuePlaceService = issuePlaceService;
            _jobService = jobService;
            _maritalStatusService = maritalStatusService;
            _receptionTypeService = receptionTypeService;
            _refferReasonService = refferReasonService;
            _regionalService = regionalService;
            _rhService = rhService;
            _sectionService = sectionService;
            _sexService = sexService;
            _specialIllnessService = specialIllnessService;
            _allergyService = allergyService;
            _refferFromService = refferFromService;
            _presenterService = presenterService;
            _sharedLocalizer = sharedLocalizer;
        }

        public async Task<BaseResponseDto> GetPatientByID(long patientId)
        {
            var curPatient=await _patientRepository
                .Where(p=>p.Id==patientId)
                .Include(p=>p.Person)
                .Include(p=>p.PatientExtraInfo)
                .Select(g => Mapper.PatientMapper.Map(g))
                .FirstOrDefaultAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = curPatient

            };
        }

       
        public async Task<BaseResponseDto> FindPatient(SearchPatientDto searchPatientDto)
        {
            var query = _patientRepository
                 .Include(p => p.Person)
                 .Include(p => p.PatientExtraInfo)
                 .AsQueryable();

            if (!string.IsNullOrEmpty(searchPatientDto.FileNo))
            {
                query = query.Where(p => p.FileNo == searchPatientDto.FileNo);
            }

            if (!string.IsNullOrEmpty(searchPatientDto.NationalCode))
            {
                query = query.Where(p => p.Person.NationalCode == searchPatientDto.NationalCode);
            }

            if (searchPatientDto.HisNo>0)
            {
                query = query.Where(p => p.Hisno == searchPatientDto.HisNo);
            }

            if (!string.IsNullOrEmpty(searchPatientDto.FullName))
            {
                query = query.Where(p => searchPatientDto.FullName.Contains(p.Person.FirstName + " " + p.Person.LastName));
            }

            if (searchPatientDto.InternalId>0)
            {
                query = query.Where(p =>p.InternalId== searchPatientDto.InternalId);
            }

            var curPatient = await query
                .Select(g => Mapper.PatientMapper.Map(g))
                .FirstOrDefaultAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = curPatient

            };
        }

        public async Task<BaseResponseDto> AddAsync(PatientDto patientDto)
        {
            var resultValid = CheckValidate.Vaild<PatientDto>(new PatientValidation(_sharedLocalizer), patientDto);
            if (resultValid.Status == ResponseStatus.Fail)
            {
                return resultValid;
            }

            var person = Mapper.PatientMapper.Map(patientDto);

            await _personRepository.AddAsync(person);
           var result= await _unitOfWork.SaveChangesAsync();

            return BaseResponseDto.Success(_sharedLocalizer["PaitentForm.Response.SavePatientSucsess"]);
        }

        public async Task<BaseResponseDto> EditAsync(PatientDto patientDto)
        {
            var resultValid = CheckValidate.Vaild<PatientDto>(new PatientValidation(_sharedLocalizer), patientDto);
            if (resultValid.Status == ResponseStatus.Fail)
            {
                return resultValid;
            }

            var person = Mapper.PatientMapper.Map(patientDto);

            var curPerson = await _personRepository
                   .Where(d => d.Id == patientDto.PersonId)
                   .Include(p => p.Patient)
                   .ThenInclude(p => p.PatientExtraInfo)
               .FirstOrDefaultAsync();

            var mapPerson = Mapper.PatientMapper.Map(patientDto, curPerson);

             _personRepository.Update(mapPerson);

            await _unitOfWork.SaveChangesAsync();

            return BaseResponseDto.Success(_sharedLocalizer["PaitentForm.Response.EditPatientSucsess"]);
        }

        public async Task<PageListResponse> GetListPatient(FilterPatientDto filterPatientDto)
        {
            var query = _patientRepository
                .Include(p => p.Person)
                .Include(p => p.PatientExtraInfo)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filterPatientDto.FileNo))
            {
                query = query.Where(p =>p.FileNo == filterPatientDto.FileNo);
            }

            if (!string.IsNullOrEmpty(filterPatientDto.FullName))
            {
                query = query.Where(p => filterPatientDto.FullName.Contains(p.Person.FirstName +" "+ p.Person.LastName));
            }

            if (filterPatientDto.HisNo>0)
            {
                query = query.Where(pt => pt.Hisno ==filterPatientDto.HisNo);
            }

            if (!string.IsNullOrEmpty(filterPatientDto.NationalCode))
            {
                query = query.Where(p => p.Person.NationalCode==filterPatientDto.NationalCode);
            }

            if (filterPatientDto.SexID>0)
            {
                query = query.Where(p => p.Person.SexId == filterPatientDto.SexID);
            }

            if (!string.IsNullOrEmpty(filterPatientDto.Mobile))
            {
                query = query.Where(p => p.Person.Mobile == filterPatientDto.Mobile);
            }             


            int pageSize = 10;
            var lstPatient = await query.Select(Mapper.PatientMapper.MapListPatient)
                                .ToPagedQuery(pageSize, filterPatientDto.PageNumber).ToListAsync();
            DateTime? dt = new DateTime();
           
            return new PageListResponse
            {
                Count = lstPatient.Count(),
                Data = lstPatient,
                Status = ResponseStatus.Success
            };
        }

        public async Task<BaseResponseDto> GetPatientBaseInfo()
        {
            var patientBaseInfo = new PatientBaseInfoDto
            {
                Doctors = (List<DoctorDto>)(await _doctorService.GetAll()).Data,
                BirthPlaces = (List<BaseDto>)(await _birthPlaceService.GetAll()).Data,
                BloodGroups = (List<BaseDto>)(await _bloodGroupService.GetAll()).Data,
                Educations = (List<BaseDto>)(await _educationService.GetAll()).Data,
                Illnesses = (List<IllnessDto>)(await _illnessService.GetAll()).Data,
                GeneralStatuses = (List<BaseDto>)(await _generalStatusService.GetAll()).Data,
                IssuePlaces = (List<BaseDto>)(await _issuePlaceService.GetAll()).Data,
                Jobs = (List<BaseDto>)(await _jobService.GetAll()).Data,
                MaritalStatuses = (List<BaseDto>)(await _maritalStatusService.GetAll()).Data,
                ReceptionTypes = (List<BaseDto>)(await _receptionTypeService.GetAll()).Data,
                RefferReasones = (List<BaseDto>)(await _refferReasonService.GetAll()).Data,
                Regionals = (List<BaseDto>)(await _regionalService.GetAll()).Data,
                Rhs = (List<BaseDto>)(await _rhService.GetAll()).Data,
                Sections = (List<SectionDto>)(await _sectionService.GetAll()).Data,
                Sexs = (List<BaseDto>)(await _sexService.GetAll()).Data,
               // SpecialIllnesses = (List<BaseDto>)(await _specialIllnessService.GetAll()).Data,
                Allergies = (List<BaseDto>)(await _allergyService.GetAll()).Data,
                Presenters = (List<BaseDto>)(await _presenterService.GetAll()).Data,
                RefferFroms = (List<BaseDto>)(await _refferFromService.GetAll()).Data

            };

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = "",
                Data = patientBaseInfo
            };
        }

        public async Task<BaseResponseDto> MaxInternalId()
        {

            var lastPatient = await _patientRepository.MaxAsync(r => r.InternalId);

            return new BaseResponseDto
            {
                Data = lastPatient
            };
        }

        private BaseResponseDto Valid(PatientDto patientDto , PatientValidation patientValidation)
        {
            var validator = new PatientValidation(_sharedLocalizer);
            var resultVaild = validator.Validate(patientDto);

            if (!resultVaild.IsValid)
            {
                return new BaseResponseDto
                {
                    Status = ResponseStatus.Fail,
                    Message = resultVaild.Errors.FirstOrDefault().ErrorMessage
                };
            }
            else
            {
                return new BaseResponseDto
                {
                    Status = ResponseStatus.Success,
                };
            }
        }
    }
}
