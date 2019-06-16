using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gostar.Setting.DTO
{
    public class AreaDTO : BaseDTO
    {
        public string Name { get; set; }
        public long? CityID { get; set; }
        public int? Code { get; set; }

        public string CityName { get; set; }
        public string CountryName { get; set; }
        public long? CountryID { get; set; }
    }
}
