using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;
using Gostar.Common;
using Newtonsoft.Json;

namespace Gostar.Setting.BL.Observers.ActionDTO
{
    public class SubpartActionDTO : ActionBaseDTO
    {
        public override DTO.Enums.SettingEntity Entity
        {
            get
            {
                return DTO.Enums.SettingEntity.Subpart;
            }
        }
        // public List<SubsystemDTO> SubsystemList { get; set; }
        public SubpartDTO Subpart { get; set; }
        public override string DisplayMessage
        {
            get
            {
                return JsonConvert.SerializeObject('\u202B' + "نام زیرسیستم : " + Subpart?.SubsystemName + "، نام بخش : " + Subpart?.Name);

            }
        }
    }
}
