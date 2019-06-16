using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gostar.Setting.DTO
{
    public class RegionDTO : BaseDTO
    {
        public long? CountryID { get; set; }
        public string CountryName { get; set; }
        public long? CityID { get; set; }
        public string CityName { get; set; }
        public string Name { get; set; }
        public long? AreaID { get; set; }
        public string AreaName { get; set; }
        public int? Code { get; set; }
    }
}
