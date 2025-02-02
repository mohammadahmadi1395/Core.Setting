﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gostar.Setting.DTO
{
    public class SubsystemDTO : BaseDTO
    {
        public List<long?> IDList { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public bool? IsSystem { get; set; } = false;
        public bool? IsActive { get; set; } = true;
        public int? RunOrder { get; set; }

        public bool? IsPart { get; set; }
    }
}
