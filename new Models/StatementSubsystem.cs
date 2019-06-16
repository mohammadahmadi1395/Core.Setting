using System;
using System.Collections.Generic;

namespace Alsahab.Setting.Data.Models
{
    public partial class StatementSubsystem
    {
        public long Id { get; set; }
        public long StatementId { get; set; }
        public long SubsystemId { get; set; }

        public Statement Statement { get; set; }
        public Subsystem Subsystem { get; set; }
    }
}
