using System;
using System.Collections.Generic;
using System.Text;

namespace His.Reception.DTO
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FatherName { get; set; }
        public string NationalCode { get; set; }
        public int? SexId { get; set; }
        public string ShNo { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? MaritalStatusId { get; set; }
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public short? Age { get; set; }
        public string Email { get; set; }
        public int? BirthPlaceId { get; set; }
        public string MedicalSystemNo { get; set; }
        public int? ExpertiseId { get; set; }
        public int? PersonnelCode { get; set; }
        public string Note { get; set; }

    }
}
