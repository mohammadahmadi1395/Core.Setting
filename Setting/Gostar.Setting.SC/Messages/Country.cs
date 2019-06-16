using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;

namespace Gostar.Setting.SC.Messages
{
    public class CountryRequest: BaseRequest<CountryDTO>
    {
        public CountryFilterDTO CountryFilter { get; set; }
    }

    public class CountryResponse : BaseResponse<CountryDTO>
    {
    }

}
