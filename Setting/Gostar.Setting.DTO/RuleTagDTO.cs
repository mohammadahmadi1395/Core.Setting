using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gostar.Setting.DTO
{
    public class RuleTagDTO : BaseDTO
    {
        public long? RuleID { get; set; }
        public long? FormTypeID { get; set; }

        //
        public String RuleDescription { get; set; }
        public Gostar.Common.RuleType? RuleType { get; set; }
        public String FormTypeTitle { get; set; }
        public long? SubSystemID { get; set; }
        public String SubSystemTitle { get; set; }
    }
}
