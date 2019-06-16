using System;
using System.Collections.Generic;

namespace His.Reception.Entities.Models
{
    public partial class SpecialIllnessReception
    {
        public int Id { get; set; }
        public long? ReceptionId { get; set; }
        public int? SpecialIllnessId { get; set; }

        public virtual Receptions Reception { get; set; }
        public virtual Illness SpecialIllness { get; set; }
    }
}
