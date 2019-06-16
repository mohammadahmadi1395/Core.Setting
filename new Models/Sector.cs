using System;
using System.Collections.Generic;

namespace Alsahab.Setting.Data.Models
{
    public partial class Sector
    {
        public long Id { get; set; }
        public long RegionId { get; set; }
        public string Name { get; set; }
        public int? Code { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDeleted { get; set; }

        public Region Region { get; set; }
    }
}
