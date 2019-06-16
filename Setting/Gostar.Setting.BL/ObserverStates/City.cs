using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;

namespace Gostar.Setting.BL.Observers.ObserverStates
{
    public class CityAdd : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.CityAdd;
            }
        }
        public CityDTO City { get; set; }
    }
    public class CityEdit : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.CityEdit;
            }
        }
        public CityDTO City { get; set; }
    }
    public class CityDelete : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.CityDelete;
            }
        }
        public CityDTO City { get; set; }
    }
}
