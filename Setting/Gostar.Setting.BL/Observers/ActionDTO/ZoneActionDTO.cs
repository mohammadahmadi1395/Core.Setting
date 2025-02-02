﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;
using Gostar.Common;
using Newtonsoft.Json;

namespace Gostar.Setting.BL.Observers.ActionDTO
{
    public class ZoneActionDTO : ActionBaseDTO
    {
        public override DTO.Enums.SettingEntity Entity
        {
            get
            {
                return DTO.Enums.SettingEntity.Zone;
            }
        }
        public ZoneDTO Zone { get; set; }
        public override string DisplayMessage
        {
            get
            {
                return JsonConvert.SerializeObject('\u202B' + "عنوان والد : " + Zone?.ParentTitle + "، عنوان محدوده : " + Zone?.Title + "، نوع محدوده : " + Zone?.Type);
            }
        }
    }
}
