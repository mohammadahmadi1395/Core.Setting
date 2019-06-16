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
    public class CountryActionDTO : ActionBaseDTO
    {
        public override DTO.Enums.SettingEntity Entity
        {
            get
            {
                return DTO.Enums.SettingEntity.Country;
            }
        }
        // public List<PersonDTO> PersonList { get; set; }
        public CountryDTO Country { get; set; }
        public override string DisplayMessage
        {
            get
            {
                return JsonConvert.SerializeObject('\u202B' + "نام : " + Country?.Name + "، نام مخفف : " + Country?.ShortName + "، پیش شماره : " + Country?.PhoneCode);
            }
        }
    }
}
