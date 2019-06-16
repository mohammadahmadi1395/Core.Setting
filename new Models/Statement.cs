using System;
using System.Collections.Generic;

namespace Alsahab.Setting.Data.Models
{
    public partial class Statement
    {
        public Statement()
        {
            StatementSubsystem = new HashSet<StatementSubsystem>();
        }

        public long Id { get; set; }
        public string TagName { get; set; }
        public string PersianText { get; set; }
        public string EnglishText { get; set; }
        public string ArabicText { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; }

        public ICollection<StatementSubsystem> StatementSubsystem { get; set; }
    }
}
