using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;

namespace Gostar.Setting.SC.Messages
{
    public class BranchRequest : BaseRequest<BranchDTO>
    {
        public BranchFilterDTO BranchFilter { get; set; }
    }
    public class BranchResponse : BaseResponse<BranchDTO>
    {

    }

}
