using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gostar.Setting.DTO
{
    public class CountryDTO : BaseDTO
    {
        public string Name { get; set; }
        public int? PhoneCode { get; set; }
        public string ShortName { get; set; }
    }
}
