using His.Reception.Application.Interface;
using His.Reception.DAL.Context;
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
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DbSet<Doctors> _doctorRepository;
        public DoctorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _doctorRepository = _unitOfWork.Set<Doctors>();
           
        }

        public async Task<BaseResponseDto> AddAsync(DoctorDto doctorDto)
        {
            var doctor = Mapper.DoctorMapper.Map(doctorDto);

            await _doctorRepository.AddAsync(doctor);
            await _unitOfWork.SaveChangesAsync();

            return BaseResponseDto.Success("Insert Doctor To Database");
        }

        public async Task<BaseResponseDto> EditAsync(DoctorDto doctorDto)
        {
            var doctor = Mapper.DoctorMapper.Map(doctorDto);
            var curDoctor = await _doctorRepository.Include(d => d.Person)
                                        .FirstOrDefaultAsync(d=>d.Id==doctorDto.Id);
            var mapDoctor = Mapper.DoctorMapper.Map(curDoctor, doctorDto);

            _doctorRepository.Update(mapDoctor);
            await _unitOfWork.SaveChangesAsync();

            return BaseResponseDto.Success("Update Doctor To Database");
        }

        public async Task<BaseResponseDto> GetAll()
        {
            var lstDoctor =await _doctorRepository.Include(d => d.Person).Select(g => Mapper.DoctorMapper.Map(g)).ToListAsync();

            return new BaseResponseDto {
                Data = lstDoctor,
                Status=ResponseStatus.Success
            };
        }

        public async Task<BaseResponseDto> GetDoctoryById(int id)
        {
            var curDoctor = await _doctorRepository
                .Where(d=>d.Id==id)
                .Include(d => d.Person).Select(g => Mapper.DoctorMapper.Map(g))
                .FirstOrDefaultAsync();

            return new BaseResponseDto
            {
                Data = curDoctor,
                Status = ResponseStatus.Success
            };
        }
    }
}
