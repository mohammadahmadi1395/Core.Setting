using His.Reception.DTO;
using His.Reception.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace His.Reception.Application.Mapper
{
    public class DoctorMapper
    {
        public static DoctorDto Map(Doctors doctor)
        {
            DoctorDto doctorDto = new DoctorDto();////
            doctorDto.Id = doctor.Id;
            doctorDto.FirstName = doctor.Person.FirstName;
            doctorDto.LastName=doctor.Person.LastName;
            doctorDto.Mobile=doctor.Person.Mobile;
            doctorDto.Phone = doctor.Person.Phone;
            doctorDto.Email = doctor.Person.Email;
            doctorDto.SexId = doctor.Person.SexId;
            doctorDto.FatherName = doctor.Person.FatherName;
            doctorDto.Age = doctor.Person.Age;
            doctorDto.FatherName = doctor.Person.FatherName;

            doctorDto.ExpertiseId = doctor.ExpertiseId;
            doctorDto.MedicalSystemNo = doctor.MedicalSystemNo;
            doctorDto.PersonnelCode = doctor.PersonnelCode;
            doctorDto.Note = doctor.Note;

            return doctorDto;
        }

        public static Doctors Map(DoctorDto doctorDto)
        {
            Doctors doctor = new Doctors();

            
            doctor.Person = new Person(); 
            doctor.Person.FirstName = doctorDto.FirstName;
            doctor.Person.LastName = doctorDto.LastName;
            doctor.Person.Mobile = doctorDto.Mobile;
            doctor.Person.Phone = doctorDto.Phone;
            doctor.Person.Email = doctorDto.Email;
            doctor.Person.SexId = doctorDto.SexId;
            doctor.Person.FatherName = doctorDto.FatherName;
            doctor.Person.Age = doctorDto.Age;
            doctor.Person.FatherName = doctorDto.FatherName;

            doctor.ExpertiseId = doctorDto.ExpertiseId;
            doctor.MedicalSystemNo = doctorDto.MedicalSystemNo;
            doctor.PersonnelCode = doctorDto.PersonnelCode;
            doctor.Note = doctorDto.Note;

            return doctor;
        }

        public static Doctors Map(Doctors doctor,DoctorDto doctorDto)
        {
            doctor.Person.FirstName = doctor.Person.FirstName;
            doctor.Person.LastName = doctor.Person.LastName;
            doctor.Person.Mobile = doctor.Person.Mobile;
            doctor.Person.Phone = doctor.Person.Phone;
            doctor.Person.Email = doctor.Person.Email;
            doctor.Person.SexId = doctor.Person.SexId;
            doctor.Person.FatherName = doctor.Person.FatherName;
            doctor.Person.Age = doctor.Person.Age;
            doctor.Person.FatherName = doctor.Person.FatherName;

            doctor.ExpertiseId = doctor.ExpertiseId;
            doctor.MedicalSystemNo = doctor.MedicalSystemNo;
            doctor.PersonnelCode = doctor.PersonnelCode;
            doctor.Note = doctor.Note;

            return doctor;
        }
    }
}
