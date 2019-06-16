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
    public class AreaActionDTO : ActionBaseDTO
    {
        public override DTO.Enums.SettingEntity Entity
        {
            get
            {
                return DTO.Enums.SettingEntity.Area;
            }
        }
        // public List<CityDTO> CityList { get; set; }
        public AreaDTO Area { get; set; }
        public override string DisplayMessage
        {
            get
            {
                if (!(Area?.CityID > 0))
                    return null;
                CityDTO City = new CityBL().CityGet(new CityDTO
                {
                    ID = (long)Area?.CityID
                })?.FirstOrDefault();
                if (City != null)
                    return JsonConvert.SerializeObject('\u202B' + "کد ناحیه : " + Area?.Code + "، نام ناحیه : " + Area?.Name + "، کد شهر : " + City?.Code + "، نام شهر : " + City?.Name + "، کد کشور : " + City?.CountryPhoneCode + "، نام کشور : " + City?.CountryName);
                else return null;
            }
        }
    }
}
