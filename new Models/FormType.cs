using System;
using System.Collections.Generic;

namespace Alsahab.Setting.Data.Models
{
    public partial class FormType
    {
        public FormType()
        {
            RuleTag = new HashSet<RuleTag>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public int? EnumId { get; set; }
        public long SubSystemId { get; set; }
        public string PublicCode { get; set; }
        public string Coment { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }

        public Subsystem SubSystem { get; set; }
        public ICollection<RuleTag> RuleTag { get; set; }
    }
}
