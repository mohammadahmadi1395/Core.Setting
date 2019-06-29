using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Setting.DTO;
using Alsahab.Common.Validation;

namespace Alsahab.Setting.BL.Validation
{
    internal class BLZoneValidator : Alsahab.Setting.DTO.Validation.ZoneValidator
    {
        ZoneBL ZoneBL = new ZoneBL();
        public BLZoneValidator() : base()
        {
            RuleFor(x => x.Title).Must(NotExist).When(x => !string.IsNullOrWhiteSpace(x.Title)).WithMessage(ValidatorOptions.LanguageManager.GetString("NotExist"));
        }
        private bool NotExist(string title)
        {
            var result = ZoneBL.ZoneGet(new ZoneDTO { Title = title });
            var Count = result.Where(s => s.Title == title)?.Count();
            if (Count > 0)
            {
                return false;
            }
            return true;
        }

    }
}
