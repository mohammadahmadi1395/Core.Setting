using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;

namespace Gostar.Setting.SC.Messages
{
    public class RegionRequest : BaseRequest<RegionDTO>
    {
        public RegionFilterDTO RegionFilter { get; set; }
    }
    public class RegionResponse : BaseResponse<RegionDTO>
    {

    }

}
