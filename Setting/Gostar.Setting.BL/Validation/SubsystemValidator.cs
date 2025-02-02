﻿using Gostar.Common.Validation;
using Gostar.Setting.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gostar.Setting.BL.Validation
{
    internal class SubsystemValidator : Gostar.Setting.DTO.Validation.SubsystemValidator
    {
        public SubsystemValidator() : base()
        {
            RuleFor(x => x.Name).Must(NotExist).When(x => !string.IsNullOrWhiteSpace(x.Name)).WithMessage(ValidatorOptions.LanguageManager.GetString("NotExist"));
        }

        private bool NotExist(string title)
        {
            SubsystemBL SubsystemBL = new SubsystemBL();
            var result = SubsystemBL.SubsystemGet(new SubsystemDTO { Name = title });
            var Count = result.Where(s => s.Name == title)?.Count();
            if (Count > 0)
            {
                return false;
            }
            return true;
        }

    }
}
