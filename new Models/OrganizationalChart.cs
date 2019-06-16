using System;
using System.Collections.Generic;

namespace Alsahab.Setting.Data.Models
{
    public partial class OrganizationalChart
    {
        public OrganizationalChart()
        {
            InverseParent = new HashSet<OrganizationalChart>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        public long? ParentId { get; set; }
        public string Code { get; set; }
        public long? LeftIndex { get; set; }
        public long? RightIndex { get; set; }
        public long? Depth { get; set; }
        public string OldCode { get; set; }

        public OrganizationalChart Parent { get; set; }
        public ICollection<OrganizationalChart> InverseParent { get; set; }
    }
}
