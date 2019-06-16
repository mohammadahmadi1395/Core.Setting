using System;
using System.Collections.Generic;

namespace Alsahab.Setting.Data.Models
{
    public partial class City
    {
        public City()
        {
            Area = new HashSet<Area>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public long CountryId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }

        public Country Country { get; set; }
        public ICollection<Area> Area { get; set; }
    }
}
