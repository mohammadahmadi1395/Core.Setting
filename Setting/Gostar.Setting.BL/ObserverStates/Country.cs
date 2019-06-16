using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;

namespace Gostar.Setting.BL.Observers.ObserverStates
{
    public class CountryAdd : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.CountryAdd;
            }
        }
        public CountryDTO Country { get; set; }
    }
    public class CountryEdit : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.CountryEdit;
            }
        }
        public CountryDTO Country { get; set; }
    }
    public class CountryDelete : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.CountryDelete;
            }
        }
        public CountryDTO Country { get; set; }
    }
}
