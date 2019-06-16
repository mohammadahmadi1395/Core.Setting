using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;

namespace Gostar.Setting.BL.Observers.ObserverStates
{
    public class RegionAgentAdd : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.RegionAgentAdd;
            }
        }
        public RegionAgentDTO RegionAgent { get; set; }
    }
    public class RegionAgentEdit : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.RegionAgentEdit;
            }
        }
        public RegionAgentDTO RegionAgent { get; set; }
    }
    public class RegionAgentDelete : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.RegionAgentDelete;
            }
        }
        public RegionAgentDTO RegionAgent { get; set; }
    }
}
