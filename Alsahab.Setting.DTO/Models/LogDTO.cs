using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Common;

namespace Alsahab.Setting.DTO.Models
{
    public class LogDTO : BaseDTO//<LogDTO, Log> 
    {
        public string Time
        {
            get
            {
                return this.CreateDate == null || this.CreateDate == DateTime.MinValue ? string.Empty : ((DateTime)this.CreateDate).ToString("HH:mm:ss");
            }
        }
        public string Date
        {
            get
            {
                return this.CreateDate == null || this.CreateDate == DateTime.MinValue ? string.Empty : ((DateTime)this.CreateDate).ToString("yyyy/MM/dd");
            }
        }
        public long UserID { set; get; }
        //public string UserName { get; set; }
        public string GroupName { get; set; }
        public long? GroupID { get; set; }
        public RoleType UserRoleType { get; set; }
        public long RegistrantPersonID { get; set; }
        public string RegistrantPersonFullName { get; set; }
        //public string GroupMembersFullName { get; set; }
        //public List<long?> GroupMembersID { get; set; }
        public int ActionTypeID { set; get; }
        public string ActionTypeTitle { get; set; }
        public int EntityID { set; get; }
        public string EntityTitle { get; set; }
        public string MessageStr { get; set; }
        public long RecordID { get; set; }
        public long? BranchID { get; set; }
        public string BranchTitle { get; set; }
    }
}
