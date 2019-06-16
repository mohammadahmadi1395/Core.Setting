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
    public class BranchAddressActionDTO : ActionBaseDTO
    {
        public override DTO.Enums.SettingEntity Entity
        {
            get
            {
                return DTO.Enums.SettingEntity.BranchAddress;
            }
        } 
        public BranchAddressDTO BranchAddress { get; set; }
        public override string DisplayMessage
        {
            get
            {  
                return JsonConvert.SerializeObject('\u202B' +  "منطقه : " + BranchAddress?.ZoneDTO.Title + "، تاریخ شروع : " + BranchAddress?.StartDate.ToString());
            }
        }
    }
}
