using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Setting.DTO;
using Alsahab.Common;
using Newtonsoft.Json;
using static Alsahab.Setting.DTO.Enums;

namespace Alsahab.Setting.BL.Observers.ActionDTO
{
    public class BranchActionDTO : ActionBaseDTO<BranchDTO>
    {
        // public override SettingEntity Entity
        // {
        //     get
        //     {
        //         return SettingEntity.Branch;
        //     }
        // }
        // public BranchDTO Branch { get; set; }
        // public BranchDTO DTO {get;set;}
        public override string DisplayMessage
        {
            get
            {
                return JsonConvert.SerializeObject('\u202B' + "نام شعبه : " + DTO?.Title + "، نام سرپرست : " + DTO?.HeadMemberName);
            }
        }
    }
}
