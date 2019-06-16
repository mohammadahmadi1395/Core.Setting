using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;

namespace Gostar.Setting.SC.Messages
{

    public class CurrencyRequest : BaseRequest<CurrencyDTO>
    {
        public CurrencyFilterDTO CurrencyFilter { get; set; }
    }
    public class CurrencyResponse : BaseResponse<CurrencyDTO>
    {

    }
}