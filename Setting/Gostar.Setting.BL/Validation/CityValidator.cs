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
   internal class CityValidator : Gostar.Setting.DTO.Validation.CityValidator
    {
        public CityValidator() : base()
        {
            RuleFor(x => x.CountryID).Must(CountryNotExist).When(x => x.CountryID > 0).WithMessage(ValidatorOptions.LanguageManager.GetString("NotExist"));
            RuleFor(x => x.Name).Must(NameNotExist).When(x => !string.IsNullOrWhiteSpace(x.Name)).WithMessage(ValidatorOptions.LanguageManager.GetString("NotExist"));
            RuleFor(x => x.Code).Must((DTO, Code) => CodeNotExist(Code, DTO.CountryID)).When(x => x.Code > 0 && x.CountryID > 0).WithMessage(ValidatorOptions.LanguageManager.GetString("NotExist"));


        }
        private bool CountryNotExist(long? CountryID)
        {
            CountryDA CountryDA = new CountryDA();
            var CountryExist = CountryDA.CountryGet(new CountryDTO { ID = CountryID }, null)?.Count();
            if (!(CountryExist > 0))
            {
                return false;
            }
            return true;
        }
        private bool NameNotExist(string name)
        {
            CityBL CityBL = new CityBL();
            var result = CityBL.CityGet(new CityDTO { Name = name });
            var Count = result?.Where(s => s.Name == name)?.Count();
            if (Count > 0)
            {
                return false;
            }
            return true;
        }
        private bool CodeNotExist(int? code, long? CountryID)
        {
            CityBL CityBL = new CityBL();
            var result = CityBL.CityGet(new CityDTO { Code = code, CountryID = CountryID });
            var Count = result?.Where(s => s.Code == code && s.CountryID == CountryID)?.Count();
            if (Count > 0)
            {
                return false;
            }
            return true;
        }
    }
}
