using Alsahab.Setting.DTO;
using Alsahab.Setting.Entities.Models;
using Alsahab.Setting.DL.Interfaces;
using Alsahab.Common;

namespace Alsahab.Setting.BL
{
    public class OrganizationalChartBL : BaseTreeBL<OrganizationalChart, OrganizationalChartDTO, OrganizationalChartFilterDTO>
    {
        private readonly IBaseDL<OrganizationalChart, OrganizationalChartDTO, OrganizationalChartFilterDTO> _OrganizationalChartDL;
        public OrganizationalChartBL(IBaseDL<OrganizationalChart, OrganizationalChartDTO, OrganizationalChartFilterDTO> organizationalChartDL,
                                IBaseDL<Entities.Models.Log, LogDTO, LogFilterDTO> logDL)
            : base(organizationalChartDL, logDL)
        {
            _OrganizationalChartDL = organizationalChartDL;
            NeedToAutoCode = true;
        }
    }
}
