using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alsahab.Setting.MyAPI.Models
{
    public class FormTypeFilterDTO : BaseFilterDTO 
    {
        public List<Enums.RequestType> RequestTypelist { get; set; }
    }
}
