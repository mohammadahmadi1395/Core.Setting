
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
    internal class BLSubpartValidator : BaseBLValidator<Subpart, SubpartDTO, SubpartFilterDTO>//: Alsahab.Setting.DTO.SubpartValidator
    {
        private readonly IBaseDL<Subpart, SubpartDTO, SubpartFilterDTO> _SubpartDL;
        public BLSubpartValidator(IBaseDL<Subpart, SubpartDTO, SubpartFilterDTO> subpartDL) : base(subpartDL)
        {
            _SubpartDL = subpartDL;
            RuleFor(x => x.Name).Must((DTO, Name) => NotExist(DTO, DTO.Name)).When(x => !string.IsNullOrWhiteSpace(x.Name)).WithMessage(ValidatorOptions.LanguageManager.GetString("AlreadyIsExists"));
        }
        private bool NotExist(SubpartDTO dto, string title)
        {            
            var result = _SubpartDL.Get(new SubpartFilterDTO { Name = title, SubsystemID = dto.SubsystemID });
            var Count = result.Where(s => s.Name == title)?.Count();
            return !(Count > 0);
            {
                return false;
            }
            return true;
        }

    }
}
