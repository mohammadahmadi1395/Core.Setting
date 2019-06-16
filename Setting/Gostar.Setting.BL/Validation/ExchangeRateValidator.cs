using Gostar.Common.Validation;
using Gostar.Setting.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gostar.Setting.BL.Validation
{
   internal class ExchangeRateValidator :Gostar.Setting.DTO.Validation.ExchangeRateValidator
    {
        public ExchangeRateValidator() : base()
        {
            RuleFor(x => x).Must(CheckExchange).When(x => x.FromCurrencyID > 0 && x.ToCurrencyID > 0 && (x.Year > DateTime.MinValue)).WithMessage(ValidatorOptions.LanguageManager.GetString("NotExist"));
        }
        private bool CheckExchange(ExchangeRateDTO data)
        {
            ExchangeRateBL ExchangeRateBL = new ExchangeRateBL();
            var Res = ExchangeRateBL.ExchangeRateGet(new ExchangeRateDTO { FromCurrencyID = data.FromCurrencyID, ToCurrencyID = data.ToCurrencyID, Year = data.Year }, null).Count;
            if (Res > 0)
            {
                return false;
            }
            return true;
        }
    }
}
