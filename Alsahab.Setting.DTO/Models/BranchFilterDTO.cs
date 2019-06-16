using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alsahab.Setting.DTO.Models
{
    public class BranchFilterDTO : BaseFilterDTO
    {
        public List<long?> IDList { get; set; }
        public bool IsLeafNode { get; set; } = true;
    }
}
