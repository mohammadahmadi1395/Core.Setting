using System;
using System.Collections.Generic;

namespace Alsahab.Setting.Data.Models
{
    public partial class RegionAgent
    {
        public long Id { get; set; }
        public DateTime StartDate { get; set; }
        public long PersonId { get; set; }
        public long RegionId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreateDate { get; set; }

        public Region Region { get; set; }
    }
}
