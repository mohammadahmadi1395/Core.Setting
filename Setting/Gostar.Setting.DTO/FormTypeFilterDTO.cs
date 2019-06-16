using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gostar.Setting.DTO
{
    public class FormTypeFilterDTO : BaseDTO 
    {
        public List<DTO.Enums.RequestType> RequestTypelist { get; set; }
    }
}
