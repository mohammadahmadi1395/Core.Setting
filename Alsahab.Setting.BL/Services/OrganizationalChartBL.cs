using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Data;
using Alsahab.Setting.Entities.Models;
using Alsahab.Setting.Data.Interfaces;
using Alsahab.Setting.BL.Validation;
using System.Threading;
using Alsahab.Common.Exceptions;
using Alsahab.Common;

namespace Alsahab.Setting.BL
{
    public class OrganizationalChartBL : BaseBL<OrganizationalChart, OrganizationalChartDTO, OrganizationalChartFilterDTO>
    {
        private readonly IBaseDL<OrganizationalChart, OrganizationalChartDTO, OrganizationalChartFilterDTO> _OrganizationalChartDL;
        public OrganizationalChartBL(IBaseDL<OrganizationalChart, OrganizationalChartDTO, OrganizationalChartFilterDTO> organizationalChartDL,
                                IBaseDL<Entities.Models.Log, LogDTO, LogFilterDTO> logDL)
            : base(organizationalChartDL, logDL)
        {
            _OrganizationalChartDL = organizationalChartDL;
            FormHasTree = true;
            NeedToAutoCode = true;
        }
    }
}
