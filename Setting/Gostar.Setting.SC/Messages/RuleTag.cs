using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;

namespace Gostar.Setting.SC.Messages
{
    public class RuleTagRequest : BaseRequest<RuleTagDTO>
    {
        public RuleTagFilterDTO RuleTagFilter { get; set; }
    }
    public class RuleTagResponse : BaseResponse<RuleTagDTO>
    {

    }
}
