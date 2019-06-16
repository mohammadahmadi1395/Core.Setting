using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Common;

namespace Gostar.Setting.DTO
{
    public class LogFilterDTO : BaseDTO
    {
        public DateTime? FromDate { set; get; }
        public DateTime? ToDate { set; get; }
        //public TimeSpan FromTime { set; get; }
        //public TimeSpan ToTime { set; get; }
        public List<long> UserIDS { set; get; }
        public List<int> ActionTypeIDs { set; get; }
        public List<int> EntityIDs { set; get; }
        public string Message { set; get; }
        public List<long> BranchIDs { set; get; }
        public List<long> GroupIDs { set; get; }
        public string FullName { set; get; }
        public List<int> UserRoleTypes { get; set; }
    }
}
