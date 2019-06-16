using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gostar.Setting.DTO
{
    public class BranchFilterDTO : BaseDTO
    {
        public List<long?> IDList { get; set; }
        public bool IsLeafNode { get; set; } = true;
    }
}
