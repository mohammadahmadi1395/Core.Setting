using System;
using System.Collections.Generic;

namespace Alsahab.Setting.DTO
{
    public class BranchRegionWorkFilterDTO : BranchRegionWorkDTO
    {
        public DateTime? CreateDateFrom { get; set; }
        public DateTime? CreateDateTo { get; set; }
        public List<long?> IDList { get; set; }
    }
}