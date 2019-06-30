using Alsahab.Common.Validation;
using Alsahab.Setting.Data.Interfaces;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Entities.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alsahab.Setting.BL.Validation
{
    internal class BLRuleValidator : Alsahab.Setting.DTO.RuleValidator
    {
        private readonly IBaseDL<Rule, RuleDTO, RuleFilterDTO> _RuleDL;
        public BLRuleValidator(IBaseDL<Rule, RuleDTO, RuleFilterDTO> ruleDL) : base()
        {
            _RuleDL = ruleDL;
            RuleFor(x => x.Title).Must(NotExist).When(x => !string.IsNullOrWhiteSpace(x.Title)).WithMessage(ValidatorOptions.LanguageManager.GetString("NotExist"));
        }
        private bool NotExist(string title)
        {
            var result = _RuleDL.Get(new RuleFilterDTO { Title = title });
            var Count = result.Where(s => s.Title.Equals(title))?.Count();
            return !(Count > 0);
        }

    }
}
