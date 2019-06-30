using System;
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

    internal class BLOrganizationalChartValidator : Alsahab.Setting.DTO.OrganizationalChartValidator
    {
        private readonly IBaseDL<OrganizationalChart, OrganizationalChartDTO, OrganizationalChartFilterDTO> _OrganizationalChartDL;
        public BLOrganizationalChartValidator(IBaseDL<OrganizationalChart, OrganizationalChartDTO, OrganizationalChartFilterDTO> organizationalChartDL) : base()
        {
            _OrganizationalChartDL = organizationalChartDL;
            RuleFor(x => x.Title).Must(NotExist).When(x => !string.IsNullOrWhiteSpace(x.Title)).WithMessage(ValidatorOptions.LanguageManager.GetString("NotExist"));
        }
        private bool NotExist(string title)
        {
            var result = _OrganizationalChartDL.Get(new OrganizationalChartFilterDTO { Title = title });
            var Count = result.Where(s => s.Title.Equals(title))?.Count();
            if (Count > 0)
                return false;
            return true;
        }
    }
}
