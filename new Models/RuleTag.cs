using System;
using System.Collections.Generic;

namespace Alsahab.Setting.Data.Models
{
    public partial class RuleTag
    {
        public long Id { get; set; }
        public long RuleId { get; set; }
        public long FormTypeId { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDeleted { get; set; }

        public FormType FormType { get; set; }
        public Rule Rule { get; set; }
    }
}
