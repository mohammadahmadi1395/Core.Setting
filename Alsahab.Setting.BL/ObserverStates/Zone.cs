using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Setting.DTO;

namespace Alsahab.Setting.BL.Observers.ObserverStates
{
    public class ZoneAdd : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.ZoneAdd;
            }
        }
        public ZoneDTO Zone { get; set; }
    }
    public class ZoneEdit : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.ZoneEdit;
            }
        }
        public ZoneDTO Zone { get; set; }
    }
    public class ZoneDelete : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.ZoneDelete;
            }
        }
        public ZoneDTO Zone { get; set; }
    }
}
