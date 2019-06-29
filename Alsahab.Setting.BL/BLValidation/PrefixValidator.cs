using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Setting.DTO;
using Alsahab.Common.Validation;
using System.Globalization;

namespace Alsahab.Setting.BL.Validation
{

    internal class BLPrefixValidator : Alsahab.Setting.DTO.Validation.PrefixValidator
    {
        PrefixBL PrefixBL = new PrefixBL();

        public BLPrefixValidator() : base()
        {
            RuleFor(x => x.Title).Must(NotExist).When(x => !string.IsNullOrWhiteSpace(x.Title)).WithMessage(ValidatorOptions.LanguageManager.GetString("NotExist"));
            RuleFor(x => x.IsDefault).Must(DefaltCount).When(x => x.IsDefault.HasValue && x.IsDefault == true).WithMessage(ValidatorOptions.LanguageManager.GetString("Default"));
        }
        private bool NotExist(string title)
        {
            var result = PrefixBL.PrefixGet(new PrefixDTO { Title = title });
            var Count = result.Where(s => s.Title == title)?.Count();
            if (Count > 0)
            {
                return false;
            }
            return true;
        }
        private bool DefaltCount(bool? isDefault)
        {
            if (!isDefault.HasValue)
                return true;
            var Count = PrefixBL.PrefixGet(new PrefixDTO { IsDefault = true })?.Count();
            if (Count > 0)
            {
                return false;
            }
            return true;
        }
    }
}
