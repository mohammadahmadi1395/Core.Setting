﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gostar.Setting.DTO
{
    public class CountryFilterDTO : BaseDTO
    {
        public int? FromPhoneCode { get; set; }
        public int? ToPhoneCode { get; set; }
    }
}
