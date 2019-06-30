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
    internal class BLOrganizationTypeValidator : Alsahab.Setting.DTO.OrganizationTypeValidator
    {
        private readonly IBaseDL<OrganizationType, OrganizationTypeDTO, OrganizationTypeFilterDTO> _OrganizationTypeDL;
        public BLOrganizationTypeValidator(IBaseDL<OrganizationType, OrganizationTypeDTO, OrganizationTypeFilterDTO> organizationType) : base()
        {
            _OrganizationTypeDL = organizationType;
            RuleFor(x => x.Title).Must(NotExist).When(x => !string.IsNullOrWhiteSpace(x.Title)).WithMessage(ValidatorOptions.LanguageManager.GetString("NotExist"));
        }

        private bool NotExist(string title)
        {            
            var result = _OrganizationTypeDL.Get(new OrganizationTypeFilterDTO { Title = title })?.ToList();
            var Count = result.Where(s => s.Title == title)?.Count();
            return !(Count > 0);
        }

    }
}
