using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;

namespace Gostar.Setting.SC.Messages
{
    public class SectorRequest : BaseRequest<SectorDTO>
    {
        public SectorFilterDTO SectorFilter { get; set; }
    }
    public class SectorResponse : BaseResponse<SectorDTO>
    {

    }
}
