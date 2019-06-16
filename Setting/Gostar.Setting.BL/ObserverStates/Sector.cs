using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;

namespace Gostar.Setting.BL.Observers.ObserverStates
{
    public class SectorAdd : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.SectorAdd;
            }
        }
        public SectorDTO Sector { get; set; }
    }
    public class SectorEdit : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.SectorEdit;
            }
        }
        public SectorDTO Sector { get; set; }
    }
    public class SectorDelete : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.SectorDelete;
            }
        }
        public SectorDTO Sector { get; set; }
    }
}
