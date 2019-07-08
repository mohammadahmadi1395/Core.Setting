using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Setting.DTO;
using Alsahab.Common.Validation;
using Alsahab.Setting.DL.Interfaces;
using Alsahab.Setting.Entities.Models;
using FluentValidation;

namespace Alsahab.Setting.BL.BLValidation
{
    internal class OrganizationTypeBLValidator : BaseBLValidator<OrganizationType, OrganizationTypeDTO, OrganizationTypeFilterDTO>//: Alsahab.Setting.DTO.OrganizationTypeValidator
    {
        private readonly IBaseDL<OrganizationType, OrganizationTypeDTO, OrganizationTypeFilterDTO> _OrganizationTypeDL;
        public OrganizationTypeBLValidator(IBaseDL<OrganizationType, OrganizationTypeDTO, OrganizationTypeFilterDTO> organizationType) : base(organizationType)
        {
            _OrganizationTypeDL = organizationType;
            RuleFor(x => x.Title).Must(NotExist).When(x => !string.IsNullOrWhiteSpace(x.Title)).WithMessage(ValidatorOptions.LanguageManager.GetString("AlreadyIsExists"));
        }

        private bool NotExist(string title)
        {            
            var result = _OrganizationTypeDL.Get(new OrganizationTypeFilterDTO { Title = title })?.ToList();
            var Count = result.Where(s => s.Title == title)?.Count();
            return !(Count > 0);
        }

    }
}
