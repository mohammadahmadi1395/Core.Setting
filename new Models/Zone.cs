using System;
using System.Collections.Generic;

namespace Alsahab.Setting.Data.Models
{
    public partial class Zone
    {
        public Zone()
        {
            BranchAddress = new HashSet<BranchAddress>();
            BranchRegionWork = new HashSet<BranchRegionWork>();
            InverseParent = new HashSet<Zone>();
        }

        public long Id { get; set; }
        public string Code { get; set; }
        public long? ParentId { get; set; }
        public string Title { get; set; }
        public int Type { get; set; }
        public string Comment { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDeleted { get; set; }
        public long? LeftIndex { get; set; }
        public long? RightIndex { get; set; }
        public long? Depth { get; set; }
        public string OldCode { get; set; }

        public Zone Parent { get; set; }
        public ICollection<BranchAddress> BranchAddress { get; set; }
        public ICollection<BranchRegionWork> BranchRegionWork { get; set; }
        public ICollection<Zone> InverseParent { get; set; }
    }
}
