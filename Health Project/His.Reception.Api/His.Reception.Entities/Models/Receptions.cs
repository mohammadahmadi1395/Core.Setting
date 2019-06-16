using System;
using System.Collections.Generic;

namespace His.Reception.Entities.Models
{
    public partial class Receptions
    {
        public Receptions()
        {
            ReceptionService = new HashSet<ReceptionService>();
            SpecialIllnessReception = new HashSet<SpecialIllnessReception>();
        }

        public long Id { get; set; }
        public int? PatientId { get; set; }
        public long? ReceptionId { get; set; }
        public DateTime? ReceptionDate { get; set; }
        public int? SectionId { get; set; }
        public int? ReceptionTypeId { get; set; }
        public int? BedDoctorId { get; set; }
        public int? DoctorId { get; set; }
        public DateTime? RefferDate { get; set; }
        public int? RefferReasonId { get; set; }
        public int? CurrentIllnessId { get; set; }
        public int? GeneralStatusId { get; set; }
        public string Advice { get; set; }
        public int? RefferFromId { get; set; }
        public string Note { get; set; }
        public int? PresenterId { get; set; }

        public virtual Doctors BedDoctor { get; set; }
        public virtual Illness CurrentIllness { get; set; }
        public virtual Doctors Doctor { get; set; }
        public virtual GeneralStatus GeneralStatus { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Presenter Presenter { get; set; }
        public virtual ReceptionType ReceptionType { get; set; }
        public virtual RefferFrom RefferFrom { get; set; }
        public virtual RefferReason RefferReason { get; set; }
        public virtual Section Section { get; set; }
        public virtual ICollection<ReceptionService> ReceptionService { get; set; }
        public virtual ICollection<SpecialIllnessReception> SpecialIllnessReception { get; set; }
    }
}
