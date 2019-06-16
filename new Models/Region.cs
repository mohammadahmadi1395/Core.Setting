using System;
using System.Collections.Generic;

namespace Alsahab.Setting.Data.Models
{
    public partial class Region
    {
        public Region()
        {
            RegionAgent = new HashSet<RegionAgent>();
            Sector = new HashSet<Sector>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long AreaId { get; set; }
        public int Code { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }

        public Area Area { get; set; }
        public ICollection<RegionAgent> RegionAgent { get; set; }
        public ICollection<Sector> Sector { get; set; }
    }
}
