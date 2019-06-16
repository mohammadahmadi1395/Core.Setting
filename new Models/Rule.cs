using System;
using System.Collections.Generic;

namespace Alsahab.Setting.Data.Models
{
    public partial class Rule
    {
        public Rule()
        {
            RuleTag = new HashSet<RuleTag>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }

        public ICollection<RuleTag> RuleTag { get; set; }
    }
}
