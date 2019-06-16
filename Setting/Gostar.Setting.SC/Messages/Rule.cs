using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;

namespace Gostar.Setting.SC.Messages
{
    public class RuleRequest : BaseRequest<RuleDTO>
    {
        public RuleFilterDTO RuleFilter { get; set; }
    }
    public class RuleResponse : BaseResponse<RuleDTO>
    {

    }
}
