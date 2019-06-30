﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Setting.DTO;
using Alsahab.Common.Validation;
using System.Globalization;
using Alsahab.Setting.Data.Interfaces;
using Alsahab.Setting.Entities.Models;
using FluentValidation;

namespace Alsahab.Setting.BL.Validation
{

    internal class BLPrefixValidator : Alsahab.Setting.DTO.PrefixValidator
    {
        private readonly IBaseDL<Prefix, PrefixDTO, PrefixFilterDTO> _PrefixDL;        
        public BLPrefixValidator(IBaseDL<Prefix, PrefixDTO, PrefixFilterDTO> prefixDL) : base()
        {
            _PrefixDL = prefixDL;
            RuleFor(x => x.Title).Must(NotExist).When(x => !string.IsNullOrWhiteSpace(x.Title)).WithMessage(ValidatorOptions.LanguageManager.GetString("NotExist"));
            RuleFor(x => x.IsDefault).Must(DefaltCount).When(x => x.IsDefault.HasValue && x.IsDefault == true).WithMessage(ValidatorOptions.LanguageManager.GetString("Default"));
        }
        private bool NotExist(string title)
        {
            var result = _PrefixDL.Get(new PrefixFilterDTO { Title = title });
            var Count = result.Where(s => s.Title == title)?.Count();
            return !(Count > 0);
        }

        private bool DefaltCount(bool? isDefault)
        {
            if (!isDefault.HasValue)
                return true;
            var Count = _PrefixDL.Get(new PrefixFilterDTO { IsDefault = true })?.Count();
            return !(Count > 0);
        }
    }
}
