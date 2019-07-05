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
    public class StatementActionDTO : ActionBaseDTO<StatementDTO>
    {
        // public override Enums.SettingEntity Entity
        // {
        //     get
        //     {
        //         return Enums.SettingEntity.Statement;
        //     }
        // }
        // // public List<StatementDTO> StatementList { get; set; }
        // public DTO.StatementDTO Statement { get; set; }
        public override string DisplayMessage
        {
            get
            {
                return JsonConvert.SerializeObject('\u202B' + "نام برچسب : " + DTO?.TagName + "، زیرسیستم : " + string.Join(" ، ", DTO?.SubsystemList?.Select(t => t.Name)?.ToList()) + "، متن فارسی : " + DTO.PersianText + "، متن انگلیسی : " + DTO.EnglishText + "، متن عربی : " + DTO.ArabicText);
            }
        }
    }
}
