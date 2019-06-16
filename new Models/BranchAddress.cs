using System;
using System.Collections.Generic;

namespace Alsahab.Setting.Data.Models
{
    public partial class BranchAddress
    {
        public BranchAddress()
        {
            Branch = new HashSet<Branch>();
        }

        public long Id { get; set; }
        public long ZoneId { get; set; }
        public string Address { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDeleted { get; set; }

        public Zone Zone { get; set; }
        public ICollection<Branch> Branch { get; set; }
    }
}
