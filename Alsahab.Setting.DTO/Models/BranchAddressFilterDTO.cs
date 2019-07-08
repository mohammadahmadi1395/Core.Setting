using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alsahab.Setting.DTO
{
    public class BranchAddressFilterDTO : BranchAddressDTO
    {
        public DateTime? CreateDateFrom { get; set; }
        public DateTime? CreateDateTo { get; set; }
        public List<long?> IDList { get; set; }

        public DateTime? FromStartDate { get; set; }
        public DateTime? ToStartDate { get; set; }
        public DateTime? FromEndDate { get; set; }
        public DateTime? ToEndDate { get; set; }
    }
}
