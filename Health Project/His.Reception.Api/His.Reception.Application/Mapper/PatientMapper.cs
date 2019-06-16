using His.Reception.DTO;
using His.Reception.Entities.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace His.Reception.Application.Mapper
{
    public class PatientMapper
    {
        public static Person Map(PatientDto patientDto)
        {
            Person person = new Person();
            Patient patient = new Patient();
            PatientExtraInfo patientExtraInfo = new PatientExtraInfo();

            person.Age = patientDto.Age;
            person.BirthDate = DateTime.Parse(patientDto.BirthDate);
            person.BirthPlaceId = patientDto.BirthPlaceId;
            person.Email = patientDto.Email;
            person.FatherName = patientDto.FatherName;
            person.FirstName = patientDto.FirstName;
            person.LastName = patientDto.LastName;
            person.Mobile = patientDto.Mobile;
            person.NationalCode = patientDto.NationalCode;
            person.ShNo = patientDto.ShNo;
            person.SexId = patientDto.SexId;

            patient.BloodGroupId = patientDto.BloodGroupId;
            patient.CreateDate = DateTime.Parse(patientDto.CreateDate);
            patient.FileNo = patientDto.FileNo;
            patient.Hisno = patientDto.Hisno;
            patient.InternalId = patientDto.ChronicIllnessId;
            patient.Note = patientDto.Note;
            person.MaritalStatusId = patientDto.MaritalStatusId;

            patientExtraInfo.AllergyId = patientDto.AllergyId;
            patientExtraInfo.EducationId = patientDto.EducationId;
            patientExtraInfo.Height = patientDto.Height;
            patientExtraInfo.Weight = patientDto.Weight;
            patientExtraInfo.ChronicIllnessId = patientDto.IllnessId;
            patientExtraInfo.IsDrinking = patientDto.IsDrinking;
            patientExtraInfo.IsSmoking = patientDto.IsSmoking;
            patientExtraInfo.IssuePlaceId = patientDto.IssuePlaceId;
            patientExtraInfo.JobId = patientDto.JobId;
            //patientExtraInfo.Presenter = patientDto.Presenter;
            patientExtraInfo.RegionalId = patientDto.RegionalId;
            patientExtraInfo.RhId = patientDto.RhId;
           // patientExtraInfo.SpecialIllnessId = patientDto.SpecialIllnessId;
            patientExtraInfo.WorkPhone = patientDto.WorkPhone;
            patientExtraInfo.IssueDate = DateTime.Parse(patientDto.IssueDate);

            //person. = patientDto.ShNo;
            // person. = patientDto.LastName;
            patient.PatientExtraInfo.Add(patientExtraInfo);
            person.Patient.Add(patient);

            return person;
        }

        public static PatientDto Map(Person person)
        {
            PatientDto personDto = new PatientDto();

            personDto.PersonId = person.Id;
            personDto.Age = person.Age;
            personDto.BirthDate = person.BirthDate.ToString();
            personDto.BirthPlaceId = person.BirthPlaceId;
            personDto.FatherName = person.FatherName;
            personDto.FirstName = person.FirstName;
            personDto.LastName = person.LastName;
            personDto.Mobile = person.Mobile;
            personDto.NationalCode = person.NationalCode;
            personDto.ShNo = person.ShNo;
            personDto.SexId = person.SexId;
            personDto.MaritalStatusId = person.MaritalStatusId;
            personDto.PatientId = person.Patient.FirstOrDefault().Id;
            personDto.BloodGroupId = person.Patient.FirstOrDefault().BloodGroupId;
            personDto.CreateDate = person.Patient.FirstOrDefault().CreateDate.ToString();
            personDto.FileNo = person.Patient.FirstOrDefault().FileNo;
            personDto.Hisno = person.Patient.FirstOrDefault().Hisno;
            personDto.ChronicIllnessId = person.Patient.FirstOrDefault().InternalId;
            personDto.Note = person.Patient.FirstOrDefault().Note;

            
            personDto.AllergyId = person.Patient.FirstOrDefault().PatientExtraInfo.FirstOrDefault().AllergyId;
            personDto.EducationId = person.Patient.FirstOrDefault().PatientExtraInfo.FirstOrDefault().EducationId;
            personDto.Height = person.Patient.FirstOrDefault().PatientExtraInfo.FirstOrDefault().Height;
            personDto.Weight = person.Patient.FirstOrDefault().PatientExtraInfo.FirstOrDefault().Weight;
            personDto.IllnessId = person.Patient.FirstOrDefault().PatientExtraInfo.FirstOrDefault().ChronicIllnessId;
            personDto.IsDrinking = person.Patient.FirstOrDefault().PatientExtraInfo.FirstOrDefault().IsDrinking;
            personDto.IsSmoking = person.Patient.FirstOrDefault().PatientExtraInfo.FirstOrDefault().IsSmoking;
            personDto.IssuePlaceId = person.Patient.FirstOrDefault().PatientExtraInfo.FirstOrDefault().IssuePlaceId;
            personDto.JobId = person.Patient.FirstOrDefault().PatientExtraInfo.FirstOrDefault().JobId;
            //personDto.Presenter = person.Patient.FirstOrDefault().PatientExtraInfo.FirstOrDefault().Presenter;
            personDto.RegionalId = person.Patient.FirstOrDefault().PatientExtraInfo.FirstOrDefault().RegionalId;
            personDto.RhId = person.Patient.FirstOrDefault().PatientExtraInfo.FirstOrDefault().RhId;
           // personDto.SpecialIllnessId = person.Patient.FirstOrDefault().PatientExtraInfo.FirstOrDefault().SpecialIllnessId;
            personDto.WorkPhone = person.Patient.FirstOrDefault().PatientExtraInfo.FirstOrDefault().WorkPhone;
            personDto.IssueDate = person.Patient.FirstOrDefault().PatientExtraInfo.FirstOrDefault().IssueDate.ToString();

            return personDto;
        }

        public static PatientDto Map(Patient  patient)
        {
            PatientDto personDto = new PatientDto();

            personDto.PersonId = patient.Person.Id;
            personDto.Age = patient.Person.Age;
            personDto.BirthDate = patient.Person.BirthDate.ToString();
            personDto.BirthPlaceId = patient.Person.BirthPlaceId;
            personDto.FatherName = patient.Person.FatherName;
            personDto.FirstName = patient.Person.FirstName;
            personDto.LastName = patient.Person.LastName;
            personDto.Mobile = patient.Person.Mobile;
            personDto.NationalCode = patient.Person.NationalCode;
            personDto.ShNo = patient.Person.ShNo;
            personDto.SexId = patient.Person.SexId;
            personDto.MaritalStatusId = patient.Person.MaritalStatusId;

            personDto.PatientId = patient.Id;
            personDto.BloodGroupId = patient.BloodGroupId;
            personDto.CreateDate = patient.ToString();
            personDto.FileNo = patient.FileNo;
            personDto.Hisno = patient.Hisno;
            personDto.ChronicIllnessId = patient.InternalId;
            personDto.Note = patient.Note;

            personDto.AllergyId = patient.PatientExtraInfo.FirstOrDefault()?.AllergyId;
            personDto.EducationId =patient.PatientExtraInfo.FirstOrDefault()?.EducationId;
            personDto.Height =patient.PatientExtraInfo.FirstOrDefault()?.Height;
            personDto.Weight =patient.PatientExtraInfo.FirstOrDefault()?.Weight;
            personDto.IllnessId =patient.PatientExtraInfo.FirstOrDefault()?.ChronicIllnessId;
            personDto.IsDrinking =patient.PatientExtraInfo.FirstOrDefault()?.IsDrinking;
            personDto.IsSmoking =patient.PatientExtraInfo.FirstOrDefault()?.IsSmoking;
            personDto.IssuePlaceId =patient.PatientExtraInfo.FirstOrDefault()?.IssuePlaceId;
            personDto.JobId =patient.PatientExtraInfo.FirstOrDefault()?.JobId;
            //personDto.Presenter =patient.PatientExtraInfo.FirstOrDefault().Presenter;
            personDto.RegionalId =patient.PatientExtraInfo.FirstOrDefault()?.RegionalId;
            personDto.RhId =patient.PatientExtraInfo.FirstOrDefault()?.RhId;
           // personDto.SpecialIllnessId =patient.PatientExtraInfo.FirstOrDefault().SpecialIllnessId;
            personDto.WorkPhone =patient.PatientExtraInfo.FirstOrDefault()?.WorkPhone;
            personDto.IssueDate =patient.PatientExtraInfo.FirstOrDefault()?.IssueDate.ToString();

            return personDto;
        }

        public static Person Map(PatientDto patientDto, Person person)
        {
            Patient patient = person.Patient.FirstOrDefault();
            PatientExtraInfo patientExtraInfo = patient.PatientExtraInfo.FirstOrDefault();

            person.Age = patientDto.Age;
            person.BirthDate = DateTime.Parse(patientDto.BirthDate);
            person.BirthPlaceId = patientDto.BirthPlaceId;
            person.Email = patientDto.Email;
            person.FatherName = patientDto.FatherName;
            person.FirstName = patientDto.FirstName;
            person.LastName = patientDto.LastName;
            person.Mobile = patientDto.Mobile;
            person.NationalCode = patientDto.NationalCode;
            person.ShNo = patientDto.ShNo;
            person.SexId = patientDto.SexId;

            patient.BloodGroupId = patientDto.BloodGroupId;
            patient.CreateDate = DateTime.Parse(patientDto.CreateDate);
            patient.FileNo = patientDto.FileNo;
            patient.Hisno = patientDto.Hisno;
            patient.InternalId = patientDto.ChronicIllnessId;
            patient.Note = patientDto.Note;
            person.MaritalStatusId = patientDto.MaritalStatusId;

            patientExtraInfo.AllergyId = patientDto.AllergyId;
            patientExtraInfo.EducationId = patientDto.EducationId;
            patientExtraInfo.Height = patientDto.Height;
            patientExtraInfo.Weight = patientDto.Weight;
            patientExtraInfo.ChronicIllnessId = patientDto.IllnessId;
            patientExtraInfo.IsDrinking = patientDto.IsDrinking;
            patientExtraInfo.IsSmoking = patientDto.IsSmoking;
            patientExtraInfo.IssuePlaceId = patientDto.IssuePlaceId;
            patientExtraInfo.JobId = patientDto.JobId;
            //patientExtraInfo.Presenter = patientDto.Presenter;
            patientExtraInfo.RegionalId = patientDto.RegionalId;
            patientExtraInfo.RhId = patientDto.RhId;
           // patientExtraInfo.SpecialIllnessId = patientDto.SpecialIllnessId;
            patientExtraInfo.WorkPhone = patientDto.WorkPhone;
            patientExtraInfo.IssueDate = DateTime.Parse(patientDto.IssueDate);

            //person. = patientDto.ShNo;
            // person. = patientDto.LastName;

            patient.PatientExtraInfo.Add(patientExtraInfo);
            person.Patient.Add(patient);

            return person;
        }

        public static Expression<Func<Patient,ListPatientDto>> MapListPatient
        {
            
            get
            {
                return x => new ListPatientDto
                {
                    Id = x.Id,
                    CreateDate = x.CreateDate.GetValueOrDefault().ToString("yyyy/MM/dd HH:mm:ss.fff",
                                CultureInfo.InvariantCulture),
                    FullName = $"{x.Person.FirstName} {x.Person.LastName}",
                    NationalCode = x.Person.NationalCode,
                    InternalId = x.InternalId.GetValueOrDefault(),
                    HisNo = x.Hisno,
                    Mobile = x.Person.Mobile,
                    FileNo = x.FileNo
                };
            }

            
        }
    }
}
