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
    public class StatementSubsystemActionDTO : ActionBaseDTO<StatementSubsystemDTO>
    {
        // public override Enums.SettingEntity Entity
        // {
        //     get
        //     {
        //         return Enums.SettingEntity.StatementSubsystem;
        //     }
        // }
        // // public List<StatementDTO> StatementList { get; set; }
        // public DTO.StatementSubsystemDTO StatementSubsystem { get; set; }
        public override string DisplayMessage
        {
            get
            {
                return JsonConvert.SerializeObject('\u202B' + "نام برچسب : " + DTO?.TagName + "، متن فارسی : " + DTO.PersianText + "، متن انگلیسی : " + DTO.EnglishText + "، متن عربی : " + DTO.ArabicText + "، نام زیرسیستم : " + DTO.SubsystemName);
            }
        }
    }
}
