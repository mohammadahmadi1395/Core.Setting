using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Setting.DTO;
using Alsahab.Common;
using Newtonsoft.Json;

namespace Alsahab.Setting.BL.Log.ActionDTO
{
    public class SubpartActionDTO : ActionBaseDTO<SubpartDTO>
    {
        // public override DTO.Enums.SettingEntity Entity
        // {
        //     get
        //     {
        //         return DTO.Enums.SettingEntity.Subpart;
        //     }
        // }
        // // public List<SubsystemDTO> SubsystemList { get; set; }
        // public SubpartDTO Subpart { get; set; }
        public override string DisplayMessage
        {
            get
            {
                return JsonConvert.SerializeObject('\u202B' + "نام زیرسیستم : " + DTO?.SubsystemName + "، نام بخش : " + DTO?.Name);
            }
        }
    }
}
