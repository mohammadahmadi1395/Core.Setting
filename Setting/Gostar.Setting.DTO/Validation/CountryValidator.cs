using Gostar.Common.Validation;
using Gostar.Setting.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gostar.Setting.DTO.Validation
{
    public class CountryValidator : AbstractValidator<CountryDTO>
    {
        public CountryValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.ShortName).NotEmpty();
            RuleFor(x => x.ShortName).NotEqual(x => x.Name).When(x=>!string.IsNullOrWhiteSpace(x.Name));
            RuleFor(x => x.ShortName).LessThan(x=>x.Name).When(x => !string.IsNullOrWhiteSpace(x.Name));
            RuleFor(x => x.PhoneCode).NotEmpty();
            RuleFor(x => x.PhoneCode).GreaterThan(0);
            RuleFor(x => x.IsDeleted).NotEqual(true);
        }
    }
}
