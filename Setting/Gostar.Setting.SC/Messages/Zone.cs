using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;

namespace Gostar.Setting.SC.Messages
{

    public class ZoneRequest : BaseRequest<ZoneDTO>
    {
        public ZoneFilterDTO ZoneFilter { get; set; }
    }

    public class ZoneResponse : BaseResponse<ZoneDTO>
    {
    }
}
