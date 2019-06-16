using System;
using System.Collections.Generic;
using System.Text;

namespace His.Reception.DTO
{
    public class PatientDto
    {
        public int PersonId { get; set; }
        public int PatientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string NationalCode { get; set; }
        public int? SexId { get; set; }
        public string ShNo { get; set; }
        public string BirthDate { get; set; }
        public int? MaritalStatusId { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public short? Age { get; set; }
        public string Email { get; set; }
        public int? BirthPlaceId { get; set; }
        public string Note { get; set; }
        public string CreateDate { get; set; }
        public long? Hisno { get; set; }
        public string FileNo { get; set; }
        public int? ChronicIllnessId { get; set; }
        public int? BloodGroupId { get; set; }

       // public int? SpecialIllnessId { get; set; }
       
        public int? IssuePlaceId { get; set; }
        public string IssueDate { get; set; }
        public int? NationallityId { get; set; }
        public int? RegionalId { get; set; }
        public bool? IsSmoking { get; set; }
        public bool? IsDrinking { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        //public int? BloodGroupId { get; set; }
        public int? AllergyId { get; set; }
        public string Presenter { get; set; }
        public int? IllnessId { get; set; }
        public int? JobId { get; set; }
        public int? EducationId { get; set; }
        public string WorkPhone { get; set; }
        public int? RhId { get; set; }

    }
}
