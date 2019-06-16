using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;

namespace Gostar.Setting.SC.Messages
{
    public class OrganizationalChartRequest : BaseRequest<OrganizationalChartDTO>
    {
        public OrganizationalChartFilterDTO OrganizationalChartFilter { get; set; }
    }
    public class OrganizationalChartResponse : BaseResponse<OrganizationalChartDTO>
    {
    }
}
