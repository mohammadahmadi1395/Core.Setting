using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;
namespace Gostar.Setting.SC.Messages
{
    public class AreaRequest : BaseRequest<AreaDTO>
    {
        public AreaFilterDTO AreaFilter { get; set; }
    }

    public class AreaResponse : BaseResponse<AreaDTO>
    {

    }

}
