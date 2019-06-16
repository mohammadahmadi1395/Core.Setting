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
    public class RegionActionDTO : ActionBaseDTO
    {
        public override DTO.Enums.SettingEntity Entity
        {
            get
            {
                return DTO.Enums.SettingEntity.Region;
            }
        }
        // public List<AreaDTO> AreaList { get; set; }
        public RegionDTO Region { get; set; }
        public override string DisplayMessage
        {
            get
            {
                if (Region == null)
                    return null;

                var Area = new AreaBL().AreaGet(new AreaDTO
                {
                    ID = Region?.AreaID ?? 0,
                })?.FirstOrDefault();

                var City = new CityBL().CityGet(new CityDTO
                {
                    ID = Area?.CityID ?? 0,
                })?.FirstOrDefault();
                if(City!=null)
                return JsonConvert.SerializeObject('\u202B' + "کد منطقه : " + Region?.Code + "، نام منطقه : " + Region?.Name + "، کد ناحیه : " + Area?.Code + "، نام ناحیه : " + Area?.Name + "، کد شهر : " + City?.Code + "، نام شهر : " + City?.Name + "، کد کشور : " + City?.CountryPhoneCode + "، نام کشور : " + City?.CountryName);
                else return null;
            }
        }
    }
}
