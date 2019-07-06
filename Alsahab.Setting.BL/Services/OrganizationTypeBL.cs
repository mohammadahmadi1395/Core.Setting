using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Alsahab.Common;
using Alsahab.Setting.BL;
using Alsahab.Setting.BL.Validation;
using Alsahab.Setting.Data.Interfaces;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Entities.Models;

namespace Alsahab.Setting.BL
{
    public class OrganizationTypeBL : BaseBL<OrganizationType, OrganizationTypeDTO, OrganizationTypeFilterDTO>
    {
        private readonly IBaseDL<OrganizationType, OrganizationTypeDTO, OrganizationTypeFilterDTO> _OrganizationTypeDL;
        public OrganizationTypeBL(IBaseDL<OrganizationType, OrganizationTypeDTO, OrganizationTypeFilterDTO> organizationTypeDL,
                                IBaseDL<Entities.Models.Log, LogDTO, LogFilterDTO> logDL) : base(organizationTypeDL, logDL)
        {
            _OrganizationTypeDL = organizationTypeDL;
        }
    }
}
