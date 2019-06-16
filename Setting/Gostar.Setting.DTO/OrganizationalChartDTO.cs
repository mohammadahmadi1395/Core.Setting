using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gostar.Setting.DTO
{
public  class OrganizationalChartDTO : BaseDTO
    {
        public string Title { get; set; }
        public string Title_ { get { return String.Format("{0} {1} {2}", Code, arg1: " - ", arg2: Title); } }//  عنوان

        public long? ParentID { get; set; }
        public string ParentTitle { get; set; }
        public string Code { get; set; }
        public Nullable<long> LeftIndex { get; set; }
        public Nullable<long> RightIndex { get; set; }
        public Nullable<long> Depth { get; set; }
        public string OldCode { get; set; }
    }
}
