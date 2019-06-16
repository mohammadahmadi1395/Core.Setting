using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;

namespace Gostar.Setting.BL.Observers.ObserverStates
{
    public class ExchangeRateAdd : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.ExchangeRateAdd;
            }
        }
        public ExchangeRateDTO ExchangeRate { get; set; }
    }
    public class ExchangeRateEdit : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.ExchangeRateEdit;
            }
        }
        public ExchangeRateDTO ExchangeRate { get; set; }
    }
    public class ExchangeRateDelete : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.ExchangeRateDelete;
            }
        }
        public ExchangeRateDTO ExchangeRate { get; set; }
    }
}
