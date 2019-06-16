using System;
using System.Collections.Generic;

namespace Alsahab.Setting.Data.Models
{
    public partial class Area
    {
        public Area()
        {
            Region = new HashSet<Region>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long CityId { get; set; }
        public int Code { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }

        public City City { get; set; }
        public ICollection<Region> Region { get; set; }
    }
}
