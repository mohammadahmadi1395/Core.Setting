using Gostar.Common.Validation;
using Gostar.Setting.DA;
using Gostar.Setting.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gostar.Setting.BL.Validation
{
   internal class RegionValidator : Gostar.Setting.DTO.Validation.RegionValidator
    {
        public RegionValidator(): base()
        {

            RuleFor(x => x.AreaID).Must(AreaNotExist).When(x => x.AreaID > 0).WithMessage(ValidatorOptions.LanguageManager.GetString("NotExist"));
            RuleFor(x => x.Name).Must(NameNotExist).When(x => !string.IsNullOrWhiteSpace(x.Name)).WithMessage(ValidatorOptions.LanguageManager.GetString("NotExist"));
            RuleFor(x => x.Code).Must((DTO, Code) => CodeNotExist(Code, DTO.AreaID)).When(x => x.Code > 0 && x.AreaID > 0).WithMessage(ValidatorOptions.LanguageManager.GetString("NotExist"));


        }
        private bool AreaNotExist(long? areaDA)
        {
            AreaDA AreaDA = new AreaDA();
            var AreaExist = AreaDA.AreaGet(new AreaDTO { ID = areaDA }, null)?.Count();
            if (!(AreaExist > 0))
            {
                return false;
            }
            return true;
        }
        private bool NameNotExist(string name)
        {
            RegionBL RegionBL = new RegionBL();
            var result = RegionBL.RegionGet(new RegionDTO { Name = name });
            var Count = result?.Where(s => s.Name == name)?.Count();
            if (Count > 0)
            {
                return false;
            }
            return true;
        }
        private bool CodeNotExist(int? code, long? areaID)
        {
            RegionBL RegionBL = new RegionBL();
            var result = RegionBL.RegionGet(new RegionDTO { Code = code, AreaID = areaID });
            var Count = result?.Where(s => s.Code == code && s.CityID == areaID)?.Count();
            if (Count > 0)
            {
                return false;
            }
            return true;
        }

    }
}
