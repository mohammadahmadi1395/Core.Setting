using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;

namespace Gostar.Setting.BL.Observers.ObserverStates
{
    public class RuleAdd : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.RuleAdd;
            }
        }
        public RuleDTO Rule { get; set; }
    }
    public class RuleEdit : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.RuleEdit;
            }
        }
        public RuleDTO Rule { get; set; }
    }
    public class RuleDelete : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.RuleDelete;
            }
        }
        public RuleDTO Rule { get; set; }
    }
}
