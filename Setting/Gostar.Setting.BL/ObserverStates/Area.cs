using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;

namespace Gostar.Setting.BL.Observers.ObserverStates
{
    public class AreaAdd : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.AreaAdd;
            }
        }
        public AreaDTO Area { get; set; }
    }
    public class AreaEdit : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.AreaEdit;
            }
        }
        public AreaDTO Area { get; set; }
    }
    public class AreaDelete : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.AreaDelete;
            }
        }
        public AreaDTO Area { get; set; }
    }
}
