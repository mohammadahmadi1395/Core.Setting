using System;
using System.Collections.Generic;

namespace His.Reception.Entities.Models
{
    public partial class ReceptionService
    {
        public long Id { get; set; }
        public long? ReceptionId { get; set; }
        public int? ServiceId { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }

        public virtual Receptions Reception { get; set; }
        public virtual Service Service { get; set; }
    }
}
