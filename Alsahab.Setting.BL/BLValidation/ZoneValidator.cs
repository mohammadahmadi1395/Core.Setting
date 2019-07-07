using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Setting.DTO;
using Alsahab.Common.Validation;
using Alsahab.Setting.Data.Interfaces;
using Alsahab.Setting.Entities.Models;
using FluentValidation;

namespace Alsahab.Setting.BL.Validation
{
    internal class BLZoneValidator : BaseBLValidator<Zone, ZoneDTO, ZoneFilterDTO>//: Alsahab.Setting.DTO.ZoneValidator
    {
        private readonly IBaseDL<Zone, ZoneDTO, ZoneFilterDTO> _ZoneDL;
        public BLZoneValidator(IBaseDL<Zone, ZoneDTO, ZoneFilterDTO> zoneDL) : base(zoneDL)
        {
            _ZoneDL = zoneDL;
            RuleFor(x => x.Title).Must(NotExist).When(x => !string.IsNullOrWhiteSpace(x.Title)).WithMessage(ValidatorOptions.LanguageManager.GetString("AlreadyIsExists"));
        }
        private bool NotExist(string title)
        {
            var result = _ZoneDL.Get(new ZoneFilterDTO { Title = title });
            var Count = result.Where(s => s.Title == title)?.Count();
            return !(Count > 0);
        }

    }
}
