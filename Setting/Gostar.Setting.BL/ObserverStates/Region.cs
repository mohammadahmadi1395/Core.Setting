using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;

namespace Gostar.Setting.BL.Observers.ObserverStates
{
    public class RegionAdd : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.RegionAdd;
            }
        }
        public RegionDTO Region { get; set; }
    }
    public class RegionEdit : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.RegionEdit;
            }
        }
        public RegionDTO Region { get; set; }
    }
    public class RegionDelete : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.RegionDelete;
            }
        }
        public RegionDTO Region { get; set; }
    }
}
