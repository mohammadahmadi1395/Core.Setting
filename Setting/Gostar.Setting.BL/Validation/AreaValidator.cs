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
    internal class AreaValidator : Gostar.Setting.DTO.Validation.AreaValidator
    {
        public AreaValidator(): base()
        {
            RuleFor(x => x.CityID).Must(CityNotExist).When(x => x.CityID > 0).WithMessage(ValidatorOptions.LanguageManager.GetString("NotExist"));
            RuleFor(x => x.Name).Must(NameNotExist).When(x => !string.IsNullOrWhiteSpace(x.Name)).WithMessage(ValidatorOptions.LanguageManager.GetString("NotExist"));
            RuleFor(x => x.Code).Must((DTO, Code) => CodeNotExist(Code, DTO.CityID)).When(x => x.Code > 0 && x.CityID > 0).WithMessage(ValidatorOptions.LanguageManager.GetString("NotExist"));
        }

        private bool CityNotExist(long? cityID)
        {
            CityDA CityDA = new CityDA();
            var CityExist = CityDA.CityGet(new CityDTO { ID = cityID }, null)?.Count();
            if (!(CityExist > 0))
            {
                return false;
            }
            return true;
        }
        private bool NameNotExist(string name)
        {
            AreaBL AreaBL = new AreaBL();
            var result = AreaBL.AreaGet(new AreaDTO { Name = name });
            var Count = result?.Where(s => s.Name == name)?.Count();
            if (Count > 0)
            {
                return false;
            }
            return true;
        }
        private bool CodeNotExist(int? code,long? cityID)
        {
            AreaBL AreaBL = new AreaBL();
            var result = AreaBL.AreaGet(new AreaDTO { Code = code , CityID = cityID});
            var Count = result?.Where(s => s.Code == code && s.CityID== cityID)?.Count();
            if (Count > 0)
            {
                return false;
            }
            return true;
        }


    }
}
