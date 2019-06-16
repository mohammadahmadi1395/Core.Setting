using System;
using System.Collections.Generic;

namespace His.Reception.Entities.Models
{
    public partial class Doctors
    {
        public Doctors()
        {
            ReceptionsBedDoctor = new HashSet<Receptions>();
            ReceptionsDoctor = new HashSet<Receptions>();
        }

        public int Id { get; set; }
        public string MedicalSystemNo { get; set; }
        public int? PersonId { get; set; }
        public int? ExpertiseId { get; set; }
        public string Note { get; set; }
        public int? PersonnelCode { get; set; }

        public virtual Expertise Expertise { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<Receptions> ReceptionsBedDoctor { get; set; }
        public virtual ICollection<Receptions> ReceptionsDoctor { get; set; }
    }
}
