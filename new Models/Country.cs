using System;
using System.Collections.Generic;

namespace Alsahab.Setting.Data.Models
{
    public partial class Country
    {
        public Country()
        {
            City = new HashSet<City>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public int? PhoneCode { get; set; }
        public string ShortName { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDeleted { get; set; }

        public ICollection<City> City { get; set; }
    }
}
