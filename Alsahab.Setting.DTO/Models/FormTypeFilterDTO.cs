using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alsahab.Setting.DTO
{
    public class FormTypeFilterDTO : FormTypeDTO 
    {
        public List<Enums.RequestType> RequestTypelist { get; set; }
    }
}
