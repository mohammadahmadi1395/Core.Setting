﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Common;

namespace Alsahab.Setting.DTO
{
    public class GeneratedFormDTO : BaseDTO//<GeneratedFormDTO, GeneratedForm>
    {
        public String PublicCode { get; set; }
        public String PrivateCode { get; set; }
        public long? SubSystemID { get; set; }
        public long? UniqeCode { get; set; }
       
    }
}
