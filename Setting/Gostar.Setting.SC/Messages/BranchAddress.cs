using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;

namespace Gostar.Setting.SC.Messages
{

    public class BranchAddressRequest : BaseRequest<BranchAddressDTO>
    {
        public BranchAddressFilterDTO BranchAddressFilter { get; set; }
    }

    public class BranchAddressResponse : BaseResponse<BranchAddressDTO>
    {
    }

}
