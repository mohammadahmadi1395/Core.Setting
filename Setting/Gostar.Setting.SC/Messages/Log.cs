using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gostar.Setting.SC.Messages
{
    public class LogRequest : BaseRequest<Gostar.Common.LogFilterDTO>
    {
    }
    public class LogResponse : BaseResponse<Gostar.Common.LogDTO>
    {
    }
}
