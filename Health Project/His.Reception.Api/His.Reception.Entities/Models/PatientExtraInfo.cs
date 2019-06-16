using System;
using System.Collections.Generic;

namespace His.Reception.Entities.Models
{
    public partial class PatientExtraInfo
    {
        public int Id { get; set; }
        public int? PatientId { get; set; }
        public string Note { get; set; }
        public int? IssuePlaceId { get; set; }
        public DateTime? IssueDate { get; set; }
        public int? NationallityId { get; set; }
        public int? RegionalId { get; set; }
        public bool? IsSmoking { get; set; }
        public bool? IsDrinking { get; set; }
        public string Weight { get; set; }
        public string Height { get; set; }
        public int? BloodGroupId { get; set; }
        public int? AllergyId { get; set; }
        public int? ChronicIllnessId { get; set; }
        public int? JobId { get; set; }
        public int? EducationId { get; set; }
        public string WorkPhone { get; set; }
        public int? RhId { get; set; }
        public string WorkAdress { get; set; }

        public virtual Allergy Allergy { get; set; }
        public virtual Illness ChronicIllness { get; set; }
        public virtual Education Education { get; set; }
        public virtual IssuePlace IssuePlace { get; set; }
        public virtual Job Job { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Regional Regional { get; set; }
        public virtual Rh Rh { get; set; }
    }
}
