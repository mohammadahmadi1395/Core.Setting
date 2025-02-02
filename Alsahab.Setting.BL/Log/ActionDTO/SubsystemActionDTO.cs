﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Setting.DTO;
using Alsahab.Common;
using Newtonsoft.Json;

namespace Alsahab.Setting.BL.Log.ActionDTO
{
    public class SubsystemActionDTO : ActionBaseDTO<SubsystemDTO>
    {
        // public override DTO.Enums.SettingEntity Entity
        // {
        //     get
        //     {
        //         return DTO.Enums.SettingEntity.Subsystem;
        //     }
        // }
        // public List<SubsystemDTO> SubsystemList { get; set; }
        // public SubsystemDTO Subsystem { get; set; }
        public override string DisplayMessage
        {
            get
            {
                return JsonConvert.SerializeObject('\u202B' + "نام زیرسیستم : " + DTO?.Name + "، نام مخفف : " + DTO?.ShortName);
            }
        }
    }
}
