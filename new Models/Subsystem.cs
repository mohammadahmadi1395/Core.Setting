using System;
using System.Collections.Generic;

namespace Alsahab.Setting.Data.Models
{
    public partial class Subsystem
    {
        public Subsystem()
        {
            FormType = new HashSet<FormType>();
            StatementSubsystem = new HashSet<StatementSubsystem>();
            Subpart = new HashSet<Subpart>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public DateTime? CreateDate { get; set; }
        public bool? IsDeleted { get; set; }
        public string Description { get; set; }
        public bool? IsSystem { get; set; }
        public bool? IsActive { get; set; }
        public int? RunOrder { get; set; }
        public bool IsPart { get; set; }

        public ICollection<FormType> FormType { get; set; }
        public ICollection<StatementSubsystem> StatementSubsystem { get; set; }
        public ICollection<Subpart> Subpart { get; set; }
    }
}
