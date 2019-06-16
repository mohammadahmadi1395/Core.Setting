using System;
using System.Collections.Generic;

namespace Alsahab.Setting.Data.Models
{
    public partial class BranchRegionWork
    {
        public long Id { get; set; }
        public long BranchId { get; set; }
        public long ZoneId { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }

        public Branch Branch { get; set; }
        public Zone Zone { get; set; }
    }
}
