using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alsahab.Common;
using Alsahab.Setting.Data.Interfaces;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Entities;
using Alsahab.Setting.Entities.Models;
using FluentValidation;
using FluentValidation.Results;

namespace Alsahab.Setting.BL.Validation
{
    internal class BLRuleTagValidator : BaseBLValidator<RuleTag, RuleTagDTO, RuleTagFilterDTO>
    {
        private readonly IBaseDL<RuleTag, RuleTagDTO, RuleTagFilterDTO> _RuleTagDL;
        public BLRuleTagValidator(IBaseDL<RuleTag, RuleTagDTO, RuleTagFilterDTO> _ruleTagDL) : base(_ruleTagDL)
        {
            _RuleTagDL = _ruleTagDL;
            RuleFor(x => x.RuleID).Must((DTO, formTypeId) => UniqueCondition(DTO.RuleID ?? 0, DTO.FormTypeID ?? 0, DTO.ID ?? 0)).When(x => !string.IsNullOrWhiteSpace(x.Title)).WithMessage(ValidatorOptions.LanguageManager.GetString("AlreadyIsExists"));
        }

        private bool UniqueCondition(long ruleID, long formTypeID, long id)
        {
            // var ruleTag = _RuleTagDL.Get(new RuleTagFilterDTO { Title = title })?.Where(s => s.Title.Equals(title))?.ToList();
            var ruleTag = _RuleTagDL.Get(new RuleTagFilterDTO { RuleID = ruleID , FormTypeID = formTypeID})?.ToList();
            return !(ruleTag.Count > 0 && !(id > 0 && id == ruleTag.FirstOrDefault().ID));
        }
    }
}

