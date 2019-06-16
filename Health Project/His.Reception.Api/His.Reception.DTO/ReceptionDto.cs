using His.Reception.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace His.Reception.DTO
{
    public class ReceptionDto
    {
        public long Id { get; set; }
        public int PersonId { get; set; }
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public int? PresenterId { get; set; }
        public string NationalCode { get; set; }
        public int? SexId { get; set; }
       // public string ShNo { get; set; }
        public string BirthDate { get; set; }
        public int? MaritalStatusId { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public short? Age { get; set; }
        public int? BirthPlaceId { get; set; }
        public long? Hisno { get; set; }
        public string FileNo { get; set; }

        public int? SpecialIllnessId { get; set; }
        public int? CurrentIllnessId { get; set; }

        public string RefferDate { get; set; }

        public int? IllnessId { get; set; }
        public string CreateDate { get; set; }

        public int? SectionId { get; set; }
        public int? DoctorId { get; set; }
        public int? RefferReasonId { get; set; }
        public int? ReceptionTypeId { get; set; }
        public long? ReceptionId { get; set; }
        public int? InternalId { get; set; }

        public int? GeneralStatusId { get; set; }
        public string Advice { get; set; }
        public string FatherName { get; set; }
        public int? RefferFromId { get; set; }
        public string ReceptionDate { get; set; }
        

        public List<IllnessDto> Illnesses { get; set; }
    }
}
