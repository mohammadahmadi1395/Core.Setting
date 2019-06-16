using System;
using System.Collections.Generic;

namespace Alsahab.Setting.Data.Models
{
    public partial class Log
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public DateTime CreateDate { get; set; }
        public int EntityId { get; set; }
        public int ActionTypeId { get; set; }
        public long? RecordId { get; set; }
        public string Message { get; set; }
        public long? GroupId { get; set; }
        public long? RegistrantPersonId { get; set; }
        public string RegistrantPersonFullName { get; set; }
        public string GroupName { get; set; }
    }
}
