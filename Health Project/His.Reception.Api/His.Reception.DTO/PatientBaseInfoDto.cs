using System;
using System.Collections.Generic;
using System.Text;

namespace His.Reception.DTO
{
    public class PatientBaseInfoDto
    {
        public List<DoctorDto> Doctors { get; set; }
        public List<BaseDto> BirthPlaces { get; set; }
        public List<BaseDto> BloodGroups { get; set; }
        public List<BaseDto> Educations { get; set; }
        public List<BaseDto> GeneralStatuses { get; set; }
        public List<IllnessDto> Illnesses { get; set; }
        public List<BaseDto> MaritalStatuses { get; set; }
        public List<BaseDto> IssuePlaces { get; set; }
        public List<BaseDto> Jobs { get; set; }
        public List<BaseDto> ReceptionTypes { get; set; }
        public List<BaseDto> RefferReasones { get; set; }
        public List<BaseDto> Regionals { get; set; }
        public List<BaseDto> Rhs { get; set; }
        public List<SectionDto>Sections { get; set; }
        public List<BaseDto> Sexs { get; set; }
        public List<BaseDto> SpecialIllnesses { get; set; }
        public List<BaseDto> Presenters { get; set; }
        public List<BaseDto> RefferFroms { get; set; }
        public List<BaseDto> Allergies { get; set; }
    }
}
