using System;
using System.Collections.Generic;

namespace Alsahab.Setting.Data.Models
{
    public partial class Branch
    {
        public Branch()
        {
            BranchRegionWork = new HashSet<BranchRegionWork>();
        }

        public long Id { get; set; }
        public long? ParentId { get; set; }
        public long BranchAddressId { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public long? HeadPersonId { get; set; }
        public string BranchPhoneNo { get; set; }
        public string BranchEmail { get; set; }
        public bool IsCentral { get; set; }
        public string Comment { get; set; }
        public DateTime CreateDate { get; set; }
        public bool? IsDeleted { get; set; }
        public long? LeftIndex { get; set; }
        public long? RightIndex { get; set; }
        public long? Depth { get; set; }
        public string OldCode { get; set; }

        public BranchAddress BranchAddress { get; set; }
        public Branch IdNavigation { get; set; }
        public Branch InverseIdNavigation { get; set; }
        public ICollection<BranchRegionWork> BranchRegionWork { get; set; }
    }
}
