using Alsahab.Common;
using Alsahab.Setting.DL.Interfaces;
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
