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
    public class StatementActionDTO : ActionBaseDTO
    {
        public override Enums.SettingEntity Entity
        {
            get
            {
                return Enums.SettingEntity.Statement;
            }
        }
        // public List<StatementDTO> StatementList { get; set; }
        public DTO.StatementDTO Statement { get; set; }
        public override string DisplayMessage
        {
            get
            {
                return JsonConvert.SerializeObject('\u202B' + "نام برچسب : " + Statement?.TagName + "، زیرسیستم : " + string.Join(" ، ", Statement?.SubsystemList?.Select(t => t.Name)?.ToList()) + "، متن فارسی : " + Statement.PersianText + "، متن انگلیسی : " + Statement.EnglishText + "، متن عربی : " + Statement.ArabicText);
            }
        }
    }
}
