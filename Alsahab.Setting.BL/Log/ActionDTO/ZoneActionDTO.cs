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
    public class ZoneActionDTO : ActionBaseDTO<ZoneDTO>
    {
        // public override DTO.Enums.SettingEntity Entity
        // {
        //     get
        //     {
        //         return DTO.Enums.SettingEntity.Zone;
        //     }
        // }
        // public ZoneDTO Zone { get; set; }
        public override string DisplayMessage
        {
            get
            {
                return JsonConvert.SerializeObject('\u202B' + "عنوان والد : " + DTO?.ParentTitle + "، عنوان محدوده : " + DTO?.Title + "، نوع محدوده : " + DTO?.Type);
            }
        }
    }
}
