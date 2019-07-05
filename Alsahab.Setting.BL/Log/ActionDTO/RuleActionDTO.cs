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
    public class RuleActionDTO : ActionBaseDTO<RuleDTO>
    {
        // public override DTO.Enums.SettingEntity Entity
        // {
        //     get
        //     {
        //         return DTO.Enums.SettingEntity.Rule;
        //     }
        // }
        // public RuleDTO Rule { get; set; }
        public override string DisplayMessage
        {
            get
            {
                return JsonConvert.SerializeObject('\u202B' + "عنوان نقش : " + DTO?.Title + "، نوع نقش : " + DTO?.Type);
            }
        }
    }
}
