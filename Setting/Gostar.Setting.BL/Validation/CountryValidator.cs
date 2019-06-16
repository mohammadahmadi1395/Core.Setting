using Gostar.Common.Validation;
using Gostar.Setting.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gostar.Setting.BL.Validation
{
    internal class CountryValidator : Gostar.Setting.DTO.Validation.CountryValidator
    {
        public CountryValidator() :base()
        {

            RuleFor(x => x.Name).Must(NotExist).When(x => !string.IsNullOrWhiteSpace(x.Name)).WithMessage(ValidatorOptions.LanguageManager.GetString("NotExist"));


        }
        private bool NotExist(string name)
        {
            CountryBL CountryBL = new CountryBL();

            var result = CountryBL.CountryGet(new CountryDTO {Name = name });
            var Count = result.Where(s => s.Name == name)?.Count();
            if (Count > 0)
            {
                return false;
            }
            return true;
        }

    }
}
