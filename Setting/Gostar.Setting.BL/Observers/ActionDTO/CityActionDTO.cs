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
    public class CityActionDTO : ActionBaseDTO
    {
        public override DTO.Enums.SettingEntity Entity
        {
            get
            {
                return DTO.Enums.SettingEntity.City;
            }
        }
        // public List<CountryDTO> CountryList { get; set; }
        public CityDTO City { get; set; }
        public override string DisplayMessage
        {
            get
            {
                if (!(City?.CountryID > 0))
                    return null;
                CountryDTO Country = new CountryBL().CountryGet(new CountryDTO
                {
                    ID = (long)City?.CountryID
                })?.FirstOrDefault();
                return JsonConvert.SerializeObject('\u202B' + "کد : " + City?.Code + "، نام شهر : " + City?.Name + "، نام کشور : " + City?.CountryName);
            }
        }
    }
}
