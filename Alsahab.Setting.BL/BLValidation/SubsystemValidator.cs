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
    internal class BLSubsystemValidator : BaseBLValidator<Subsystem, SubsystemDTO, SubsystemFilterDTO>//Alsahab.Setting.DTO.SubsystemValidator
    {
        private readonly IBaseDL<Subsystem, SubsystemDTO, SubsystemFilterDTO> _SubsystemDL;
        public BLSubsystemValidator(IBaseDL<Subsystem, SubsystemDTO, SubsystemFilterDTO> subsystemDL) : base(subsystemDL)
        {
            _SubsystemDL = subsystemDL;
            RuleFor(x => x.Name).Must(NotExist).When(x => !string.IsNullOrWhiteSpace(x.Name)).WithMessage(ValidatorOptions.LanguageManager.GetString("AlreadyIsExists"));
        }

        private bool NotExist(string title)
        {
            var result = _SubsystemDL.Get(new SubsystemFilterDTO { Name = title });
            var Count = result.Where(s => s.Name == title)?.Count();
            return !(Count > 0);
        }

    }
}
