using System;
using System.Collections.Generic;

namespace His.Reception.Entities.Models
{
    public partial class Patient
    {
        public Patient()
        {
            PatientExtraInfo = new HashSet<PatientExtraInfo>();
            Receptions = new HashSet<Receptions>();
        }

        public int Id { get; set; }
        public int? PersonId { get; set; }
        public string Note { get; set; }
        public DateTime? CreateDate { get; set; }
        public long? Hisno { get; set; }
        public string FileNo { get; set; }
        public int? InternalId { get; set; }
        public int? BloodGroupId { get; set; }

        public virtual BloodGroup BloodGroup { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<PatientExtraInfo> PatientExtraInfo { get; set; }
        public virtual ICollection<Receptions> Receptions { get; set; }
    }
}
