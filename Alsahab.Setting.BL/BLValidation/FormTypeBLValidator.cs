
using Alsahab.Common.Validation;
using Alsahab.Setting.DL.Interfaces;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Entities.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alsahab.Setting.BL.BLValidation
{
    internal class FormTypeBLValidator : BaseBLValidator<FormType, FormTypeDTO, FormTypeFilterDTO>
    {        
        private readonly IBaseDL<FormType, FormTypeDTO, FormTypeFilterDTO> _FormTypeDL;
        public FormTypeBLValidator(IBaseDL<FormType, FormTypeDTO, FormTypeFilterDTO> formTypeDL) : base(formTypeDL)
        {
            _FormTypeDL = formTypeDL;
            RuleFor(x => x.Title).Must(NotExistTitle).When(x => !string.IsNullOrWhiteSpace(x.Title)).WithMessage(ValidatorOptions.LanguageManager.GetString("AlreadyIsExists"));
            RuleFor(x => x.PublicCode).Must(NotExistPublicCode).When(x => !string.IsNullOrWhiteSpace(x.PublicCode)).WithMessage(ValidatorOptions.LanguageManager.GetString("AlreadyIsExists"));
        }

        private bool NotExistTitle(string title)
        {
            var result = _FormTypeDL.Get(new FormTypeFilterDTO { Title = title });
            var Count = result.Where(s => s.Title == title)?.Count();
            if (Count > 0)
            {
                return false;
            }
            return true;
        }

        private bool NotExistPublicCode(string publicCode)
        {
            var result = _FormTypeDL.Get(new FormTypeFilterDTO { PublicCode = publicCode });
            var Count = result.Where(s => s.PublicCode == publicCode)?.Count();
            if (Count > 0)
            {
                return false;
            }
            return true;
        }
    }
}
