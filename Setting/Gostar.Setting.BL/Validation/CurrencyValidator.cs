using Gostar.Common.Validation;
using Gostar.Setting.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gostar.Setting.BL.Validation
{
   internal class CurrencyValidator : Gostar.Setting.DTO.Validation.CurrencyValidator
    {
        public CurrencyValidator():base()
        {

            RuleFor(x => x.Title).Must(NotExist).When(x => !string.IsNullOrWhiteSpace(x.Title)).WithMessage(ValidatorOptions.LanguageManager.GetString("NotExist"));

        }
        private bool NotExist(string title)
        {
            CurrencyBL CurrencyBL = new CurrencyBL();
            var result = CurrencyBL.CurrencyGet(new CurrencyDTO{ Title= title });
            var Count = result?.Where(s => s.Title== title)?.Count();
            if (Count > 0)
            {
                return false;
            }
            return true;
        }

    }
}
