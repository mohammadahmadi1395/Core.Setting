using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gostar.Setting.DTO
{
   public class CityDTO : BaseDTO
    {
        public string Name { get; set; }
        public int? Code { get; set; }
        public long? CountryID { get; set; }
        public int? CountryPhoneCode { get; set; }
        public string CountryName { get; set; }
    }
}
