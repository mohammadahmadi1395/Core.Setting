using Gostar.Common.Validation;
using Gostar.Setting.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gostar.Setting.DTO.Validation
{
    public class ExchangeRateValidator : AbstractValidator<ExchangeRateDTO>
    {
        public ExchangeRateValidator()
        {
            RuleFor(x => x.FromCurrencyID).NotEmpty();
            RuleFor(x => x.ToCurrencyID).NotEmpty();
            RuleFor(x => x.Ratio).NotEmpty();
            RuleFor(x => x.Year).NotEmpty().GreaterThan(DateTime.MinValue);
            RuleFor(x => x.IsDeleted).NotEqual(true);
        }
    }
}
