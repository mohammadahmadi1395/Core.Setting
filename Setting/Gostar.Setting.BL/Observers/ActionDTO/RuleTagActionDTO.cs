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
    public class RuleTagActionDTO : ActionBaseDTO
    {
        public override DTO.Enums.SettingEntity Entity
        {
            get
            {
                return DTO.Enums.SettingEntity.RuleTag;
            }
        }
        public RuleTagDTO RuleTag { get; set; }
        public override string DisplayMessage
        {
            get
            {
                return JsonConvert.SerializeObject('\u202B' + "نوع نقش : " + RuleTag?.RuleType);
            }
        }
    }
}
