using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;

namespace Gostar.Setting.SC.Messages
{
   public class CityRequest: BaseRequest<CityDTO>
    {
        public CityFilterDTO CityFilter { get; set; }
    }
    public class CityResponse : BaseResponse<CityDTO>
    {
    }

}
