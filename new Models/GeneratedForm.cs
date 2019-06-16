using System;
using System.Collections.Generic;

namespace Alsahab.Setting.Data.Models
{
    public partial class GeneratedForm
    {
        public long Id { get; set; }
        public string PublicCode { get; set; }
        public string PrivateCode { get; set; }
        public long SubsystemId { get; set; }
        public long UniqeCode { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
