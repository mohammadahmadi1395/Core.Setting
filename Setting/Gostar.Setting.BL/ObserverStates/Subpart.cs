using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;

namespace Gostar.Setting.BL.Observers.ObserverStates
{
    public class SubpartAdd : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.SubpartAdd;
            }
        }
        public SubpartDTO Subpart { get; set; }
    }
    public class SubpartEdit : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.SubpartEdit;
            }
        }
        public SubpartDTO Subpart { get; set; }
    }
    public class SubpartDelete : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.SubpartDelete;
            }
        }
        public SubpartDTO Subpart { get; set; }
    }
}
