using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alsahab.Setting.DTO
{
    public class FormTypeFilterDTO : FormTypeDTO 
    {
        public DateTime? CreateDateFrom { get; set; }
        public DateTime? CreateDateTo { get; set; }
        public List<long?> IDList { get; set; }

        public List<Enums.RequestType> RequestTypelist { get; set; }
    }
}
