using His.Reception.DTO;
using His.Reception.Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace His.Reception.Application.Mapper
{
    public class ReceptionMapper
    {
        public static Receptions Map(ReceptionDto receptionDto)
        {
            Receptions receptions = new Receptions();
            receptions.Patient = new Patient();
            receptions.Patient.Person = new Person();
            
            receptions.Advice = receptionDto.Advice;
            receptions.DoctorId = receptionDto.DoctorId;
            receptions.GeneralStatusId = receptionDto.GeneralStatusId;
           // receptions.InternalId = receptionDto.InternalId;
            receptions.ReceptionDate = DateTime.Parse(receptionDto.ReceptionDate);
            receptions.RefferDate = DateTime.Parse(receptionDto.RefferDate);
            receptions.RefferFromId = receptionDto.RefferFromId;
            receptions.SectionId = receptionDto.SectionId;
            receptions.ReceptionTypeId = receptionDto.ReceptionTypeId;
            receptions.RefferReasonId = receptionDto.RefferReasonId;
            receptions.CurrentIllnessId = receptionDto.CurrentIllnessId;
            receptions.PresenterId = receptionDto.PresenterId;

           
            foreach(var item in receptionDto.Illnesses)
            {
                receptions.SpecialIllnessReception.Add(new SpecialIllnessReception {
                 SpecialIllnessId=item.Id
                });
            }
           
            //receptions.SpecialIllnessId = receptionDto.SpecialIllnessId;

            receptions.Patient.Hisno = receptionDto.Hisno;
            receptions.Patient.FileNo = receptionDto.FileNo;
            receptions.Patient.CreateDate = DateTime.Parse(receptionDto.CreateDate);
            receptions.Patient.InternalId = receptionDto.InternalId;

            receptions.Patient.Person.FirstName = receptionDto.FirstName;
            receptions.Patient.Person.LastName = receptionDto.LastName;
            receptions.Patient.Person.MaritalStatusId = receptionDto.MaritalStatusId;
            receptions.Patient.Person.Phone = receptionDto.Phone;
            receptions.Patient.Person.Mobile = receptionDto.Mobile;
            receptions.Patient.Person.Address = receptionDto.Address;
            receptions.Patient.Person.Age = receptionDto.Age;
            receptions.Patient.Person.FatherName = receptionDto.FatherName;
            receptions.Patient.Person.BirthDate = DateTime.Parse(receptionDto.BirthDate);

            receptions.Patient.PatientExtraInfo.Add(new PatientExtraInfo());
           // receptions.s
            //receptions.Patient.Person.pe = DateTime.Parse(receptionDto.BirthDate);

            return receptions;
        }

        public static Receptions Map(Receptions receptions ,ReceptionDto receptionDto)
        {
            receptions.Patient = new Patient();
            receptions.Patient.Person = new Person();

            receptions.Advice = receptionDto.Advice;
            receptions.DoctorId = receptionDto.DoctorId;
            receptions.GeneralStatusId = receptionDto.GeneralStatusId;
            // receptions.InternalId = receptionDto.InternalId;
            receptions.ReceptionDate = DateTime.Parse(receptionDto.ReceptionDate);
            receptions.RefferDate = DateTime.Parse(receptionDto.RefferDate);
            receptions.RefferFromId = receptionDto.RefferFromId;
            receptions.SectionId = receptionDto.SectionId;
            receptions.ReceptionTypeId = receptionDto.ReceptionTypeId;
            receptions.RefferReasonId = receptionDto.RefferReasonId;
            receptions.CurrentIllnessId = receptionDto.CurrentIllnessId;
            receptions.PresenterId = receptionDto.PresenterId;


            foreach (var item in receptionDto.Illnesses)
            {
                receptions.SpecialIllnessReception.Add(new SpecialIllnessReception
                {
                    SpecialIllnessId = item.Id
                });
            }

            //receptions.SpecialIllnessId = receptionDto.SpecialIllnessId;

            receptions.Patient.Hisno = receptionDto.Hisno;
            receptions.Patient.FileNo = receptionDto.FileNo;
            receptions.Patient.CreateDate = DateTime.Parse(receptionDto.CreateDate);
            receptions.Patient.InternalId = receptionDto.InternalId;

            receptions.Patient.Person.FirstName = receptionDto.FirstName;
            receptions.Patient.Person.LastName = receptionDto.LastName;
            receptions.Patient.Person.MaritalStatusId = receptionDto.MaritalStatusId;
            receptions.Patient.Person.Phone = receptionDto.Phone;
            receptions.Patient.Person.Mobile = receptionDto.Mobile;
            receptions.Patient.Person.Address = receptionDto.Address;
            receptions.Patient.Person.Age = receptionDto.Age;
            receptions.Patient.Person.FatherName = receptionDto.FatherName;
            receptions.Patient.Person.BirthDate = DateTime.Parse(receptionDto.BirthDate);

            receptions.Patient.PatientExtraInfo.Add(new PatientExtraInfo());
            // receptions.s
            //receptions.Patient.Person.pe = DateTime.Parse(receptionDto.BirthDate);

            return receptions;
        }

        public static ReceptionDto Map(Receptions receptions)
        {
            ReceptionDto receptionDto = new ReceptionDto();             

            receptionDto.Advice = receptions.Advice;
            receptionDto.DoctorId = receptions.DoctorId;
            receptionDto.GeneralStatusId = receptions.GeneralStatusId;
            // receptions.InternalId = receptionDto.InternalId;
            receptionDto.ReceptionDate = receptions.ReceptionDate.ToString();
            receptionDto.RefferDate = receptions.RefferDate.ToString();
            receptionDto.RefferFromId = receptions.RefferFromId;
            receptionDto.SectionId = receptions.SectionId;
            receptionDto.ReceptionTypeId = receptions.ReceptionTypeId;
            receptionDto.RefferReasonId = receptions.RefferReasonId;
            receptionDto.CurrentIllnessId = receptions.CurrentIllnessId;
            receptionDto.PresenterId = receptions.PresenterId;

            foreach (var item in receptions.SpecialIllnessReception)
            {
                receptionDto.Illnesses.Add(new IllnessDto
                {
                    Id = item.SpecialIllnessId.GetValueOrDefault()
                });
            }

            //receptions.SpecialIllnessId = receptionDto.SpecialIllnessId;

            receptionDto.Hisno = receptions.Patient.Hisno;
            receptionDto.FileNo = receptions.Patient.FileNo;
            receptionDto.CreateDate = receptions.Patient.CreateDate.ToString();
            receptionDto.InternalId = receptions.Patient.InternalId;

            receptionDto.FirstName = receptions.Patient.Person.FirstName;
            receptionDto.LastName = receptions.Patient.Person.LastName;
            
            receptionDto.MaritalStatusId = receptions.Patient.Person.MaritalStatusId;
            receptionDto.Phone = receptions.Patient.Person.Phone;
            receptionDto.Mobile = receptions.Patient.Person.Mobile;
            receptionDto.Address = receptions.Patient.Person.Address;
            receptionDto.Age = receptionDto.Age;
            receptionDto.FatherName = receptionDto.FatherName;
            receptionDto.BirthDate = receptions.Patient.Person.BirthDate.ToString();            

            return receptionDto;
        }

        //public static ListReceptionDto MapListReception(Receptions receptions)
        //{
        //    ListReceptionDto listReceptionDto = new ListReceptionDto();

        //    listReceptionDto.FullName = $"{receptions.Patient.Person.FirstName} {receptions.Patient.Person.LastName}";
        //    listReceptionDto.NationalCode = receptions.Patient.Person.NationalCode;
        //    listReceptionDto.ReceptionId = receptions.ReceptionId.GetValueOrDefault();
        //    listReceptionDto.PatientId = receptions.Patient.InternalId.GetValueOrDefault();
        //    listReceptionDto.ReferDate = receptions.RefferDate.ToString();
           
        //    return listReceptionDto;
        //}

        //public static Expression<Func<ListReceptionDto, Receptions>> prog()
        //{
        //    return x => new 
        //    {
                
        //    };
        //}
        public static Expression<Func<Receptions, ListReceptionDto>> MapListReceptions
        {
            get
            {
                return x => new ListReceptionDto
                {
                    Id=x.Id,
                    FullName = $"{x.Patient.Person.FirstName} {x.Patient.Person.LastName}",
                    NationalCode = x.Patient.Person.NationalCode,
                    PatientId = x.PatientId.GetValueOrDefault(),
                    ReceptionId = x.ReceptionId.GetValueOrDefault(),
                    ReferDate = x.RefferDate.ToString()
                };
            }
        }
    }
}
