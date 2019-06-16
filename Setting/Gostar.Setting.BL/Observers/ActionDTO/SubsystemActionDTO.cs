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
    public class SubsystemActionDTO : ActionBaseDTO
    {
        public override DTO.Enums.SettingEntity Entity
        {
            get
            {
                return DTO.Enums.SettingEntity.Subsystem;
            }
        }
        // public List<SubsystemDTO> SubsystemList { get; set; }
        public SubsystemDTO Subsystem { get; set; }
        public override string DisplayMessage
        {
            get
            {
                return JsonConvert.SerializeObject('\u202B' + "نام زیرسیستم : " + Subsystem?.Name + "، نام مخفف : " + Subsystem?.ShortName);
            }
        }
    }
}
