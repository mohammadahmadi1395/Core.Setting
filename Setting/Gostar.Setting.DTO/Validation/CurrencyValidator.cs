using Gostar.Common.Validation;
using Gostar.Setting.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gostar.Setting.DTO.Validation
{
   public class CurrencyValidator : AbstractValidator<CurrencyDTO>
    {
        public CurrencyValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Symbol).NotEmpty();
            RuleFor(x => x.IsDeleted).NotEqual(true);
        }

    }
}
