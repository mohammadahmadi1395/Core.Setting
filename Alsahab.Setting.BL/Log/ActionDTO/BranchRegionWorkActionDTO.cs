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
    public class BranchRegionWorkActionDTO : ActionBaseDTO<BranchRegionWorkDTO>
    {
        // public override DTO.Enums.SettingEntity Entity
        // {
        //     get
        //     {
        //         return DTO.Enums.SettingEntity.BranchRegionWork;
        //     }
        // }
        // public BranchRegionWorkDTO BranchRegionWork { get; set; }
        public override string DisplayMessage
        {
            get
            {
                return JsonConvert.SerializeObject('\u202B' + "کد شعبه : " + DTO?.BranchID);
            }
        }

    }
}
