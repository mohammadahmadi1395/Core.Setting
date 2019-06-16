using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Gostar.Setting.DTO
{
    public class SectorDTO : BaseDTO
    {
        public String Name { get; set; }
        public int? Code { get; set; }


        //Non Required
        public String RegionName { get; set; }
        public long? RegionID { get; set; }

        public String AreaName { get; set; }
        public long? AreaID { get; set; }

        public String CityName { get; set; }
        public long? CityID { get; set; }

        public String CountryName { get; set; }
        public long? CountryID { get; set; }
        //Non Required

    }
}
