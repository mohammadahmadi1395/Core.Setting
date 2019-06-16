using Gostar.Common.Validation;
using Gostar.Setting.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gostar.Setting.BL.Validation
{
    internal class RuleValidator : Gostar.Setting.DTO.Validation.RuleValidator
    {
        public RuleValidator()
        {
            RuleFor(x => x.Title).Must(NotExist).When(x => !string.IsNullOrWhiteSpace(x.Title)).WithMessage(ValidatorOptions.LanguageManager.GetString("NotExist"));
        }
        private bool NotExist(string title)
        {
            RuleBL RuleBL = new RuleBL();
            var result = RuleBL.RuleGet(new RuleDTO { Title = title });
            var Count = result.Where(s => s.Title == title)?.Count();
            if (Count > 0)
            {
                return false;
            }
            return true;
        }

    }
}
