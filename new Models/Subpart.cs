using System;
using System.Collections.Generic;

namespace Alsahab.Setting.Data.Models
{
    public partial class Subpart
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsSystem { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public long SubsystemId { get; set; }
        public string Description { get; set; }

        public Subsystem Subsystem { get; set; }
    }
}
