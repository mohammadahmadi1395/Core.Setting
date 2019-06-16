using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;

namespace Gostar.Setting.BL.Observers.ObserverStates
{
    public class RuleTagAdd : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.RuleTagAdd;
            }
        }
        public RuleTagDTO RuleTag { get; set; }
    }
    public class RuleTagEdit : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.RuleTagEdit;
            }
        }
        public RuleTagDTO RuleTag { get; set; }
    }
    public class RuleTagDelete : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.RuleTagDelete;
            }
        }
        public RuleTagDTO RuleTag { get; set; }
    }
}
