using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;

namespace Gostar.Setting.SC.Messages
{

    public class ExchangeRateRequest : BaseRequest<ExchangeRateDTO>
    {
        public ExchangeRateFilterDTO ExchangeRateFilter { get; set; }
    }
    public class ExchangeRateResponse : BaseResponse<ExchangeRateDTO>
    {

    }
}
