using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;

namespace Gostar.Setting.BL.Observers.ObserverStates
{
    public class CurrencyAdd : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.CurrencyAdd;
            }
        }
        public CurrencyDTO Currency { get; set; }
    }
    public class CurrencyEdit : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.CountryEdit;
            }
        }
        public CurrencyDTO Currency { get; set; }
    }
    public class CurrencyDelete : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.CountryDelete;
            }
        }
        public CurrencyDTO Currency { get; set; }
    }
}
