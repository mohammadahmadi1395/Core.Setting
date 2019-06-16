using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Common;

namespace Gostar.Setting.DTO
{
    public class RuleDTO : BaseDTO
    {
        public String Title { get; set; }
        public String Description { get; set; }
        public RuleType? Type { get; set; }
        

    }
}
