using His.Reception.Application.Interface;
using His.Reception.Application.Mapper;
using His.Reception.DAL.Context;
using His.Reception.DAL.Extensions;
using His.Reception.DTO;
using His.Reception.DTO.Message;
using His.Reception.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace His.Reception.Application.Service
{
    public class ReceptionsService : IReceptionsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<Receptions> _receptionRepository;
        private readonly DbSet<ReceptionService> _receptionServiceRepository;
        private readonly IPatientService _patientService;
        public ReceptionsService(IUnitOfWork unitOfWork, IPatientService patientService)
        {
            _unitOfWork = unitOfWork;
            _receptionRepository = _unitOfWork.Set<Receptions>();
            _patientService = patientService;
            _receptionServiceRepository = _unitOfWork.Set<ReceptionService>();
        }

        public async Task<BaseResponseDto> AddAsync(ReceptionDto receptionDto)
        {
            var reception = ReceptionMapper.Map(receptionDto);
            var maxReceptionId = (long)(await MaxReceptionId())?.Data;
            var maxIntenralId = (int)(await _patientService.MaxInternalId()).Data;

            reception.ReceptionId = ++maxReceptionId;
            reception.Patient.InternalId = ++maxIntenralId;

            await _receptionRepository.AddAsync(reception);

            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = "Insert Reception "

            };
        }

        public async Task<BaseResponseDto> GetListReception(FilterReceptionDto filterReceptionDto)
        {
            var query = _receptionRepository
                .Include(p => p.Patient)
                .ThenInclude(p => p.Person)
                .AsQueryable();


            if (filterReceptionDto.ReceptionId > 0)
            {
                query = query.Where(p => p.ReceptionId == filterReceptionDto.ReceptionId);
            }

            if (filterReceptionDto.HisNo > 0)
            {
                query = query.Where(p => p.Patient.Hisno == filterReceptionDto.HisNo);
            }

            if (!string.IsNullOrEmpty(filterReceptionDto.FileNo))
            {
                query = query.Where(p => p.Patient.FileNo.Contains(filterReceptionDto.FileNo));
            }

            if (filterReceptionDto.InternalId > 0)
            {
                query = query.Where(p => p.Patient.InternalId == filterReceptionDto.InternalId);
            }

            if (!string.IsNullOrEmpty(filterReceptionDto.Mobile))
            {
                query = query.Where(p => p.Patient.FileNo.Contains(filterReceptionDto.Mobile));
            }

            if (!string.IsNullOrEmpty(filterReceptionDto.ReceptionStartDate) && !string.IsNullOrEmpty(filterReceptionDto.ReceptionEndDate))
            {
                query = query.Where(r => r.ReceptionDate >= DateTime.Parse(filterReceptionDto.ReceptionStartDate) && r.ReceptionDate <= DateTime.Parse(filterReceptionDto.ReceptionEndDate));
            }

            if (filterReceptionDto.DoctorId > 0)
            {
                query = query.Where(r => r.DoctorId == filterReceptionDto.DoctorId);
            }

            if (filterReceptionDto.IsToday)
            {
                query = query.Where(r => r.ReceptionDate >= DateTime.Parse(filterReceptionDto.ReceptionStartDate) && r.ReceptionDate <= DateTime.Parse(filterReceptionDto.ReceptionEndDate));
            }

            // var lstReception =await query.Select(g=>Mapper.ReceptionMapper.MapListReception(g)).ToPagedQuery(10, filterReceptionDto.PageNumber).ToListAsync();
            //var lstReception =await query.Select(d=> new { d.ReceptionId} ).ToPagedQuery(10, filterReceptionDto.PageNumber).ToListAsync();
            var lstReception = await query.Select(Mapper.ReceptionMapper.MapListReceptions).ToPagedQuery(10, filterReceptionDto.PageNumber).ToListAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = lstReception
            };
        }

        public async Task<BaseResponseDto> MaxReceptionId()
        {
            var lastReception = await _receptionRepository.MaxAsync(r => r.ReceptionId);

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Data = lastReception
            };
        }


        public async Task<BaseResponseDto> AddReceptionServices(List<ReceptionServiceDto> receptionServiceDto)
        {
            List<ReceptionService> lstReceptionService = new List<ReceptionService>();
            foreach (var item in receptionServiceDto)
            {
                lstReceptionService.Add(new ReceptionService
                {
                    Price = item.Price,
                    Quantity = item.Quantity,
                    ReceptionId = item.ReceptoinId,
                    ServiceId = item.ServiceId
                });
            }
            await _receptionServiceRepository.AddRangeAsync(lstReceptionService);

            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = "Insert Service For Reception"
            };
        }

        public async Task<BaseResponseDto> EditAsync(ReceptionDto receptionDto)
        {
            var curReception = await _receptionRepository.FirstOrDefaultAsync(r => r.Id == receptionDto.Id);

            var mapReception = Mapper.ReceptionMapper.Map(curReception, receptionDto);

            _receptionRepository.Update(mapReception);
            await _unitOfWork.SaveChangesAsync();

            return new BaseResponseDto
            {
                Status = ResponseStatus.Success,
                Message = "Update Reception"
            };
        }
    }
}
