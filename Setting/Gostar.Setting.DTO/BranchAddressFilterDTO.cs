using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gostar.Setting.DTO
{
    public class BranchAddressFilterDTO : BaseDTO
    {
        public DateTime? FromStartDate { get; set; }
        public DateTime? ToStartDate { get; set; }
        public DateTime? FromEndDate { get; set; }
        public DateTime? ToEndDate { get; set; }
    }
}
