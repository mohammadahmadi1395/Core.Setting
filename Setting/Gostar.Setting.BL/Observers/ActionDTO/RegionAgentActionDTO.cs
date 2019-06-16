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
    public class RegionAgentActionDTO : ActionBaseDTO
    {
        public override DTO.Enums.SettingEntity Entity
        {
            get
            {
                return DTO.Enums.SettingEntity.RegionAgent;
            }
        }
        public RegionAgentDTO RegionAgent { get; set; }
        public override string DisplayMessage
        {
            get
            {
                if (RegionAgent == null)
                    return null;

                var Region = new RegionBL()?.RegionGet(new RegionDTO
                {
                    ID = RegionAgent?.RegionID ?? 0,
                });
                if (Region != null)
                    return JsonConvert.SerializeObject('\u202B' + "نام مسئول ناحیه : " + RegionAgent?.AgentFullName + "، مسئولیت از : " + RegionAgent?.StartDate + " تا : " + RegionAgent?.EndDate + "، نام منطقه : " + RegionAgent?.RegionName + "، نام ناحیه : " + RegionAgent?.AreaName + "، نام شهر : " + RegionAgent?.CityName + "، نام کشور : " + RegionAgent?.CountryName);
                else return null;
            }
        }
    }
}
