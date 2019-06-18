using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Setting.DTO;
using static Alsahab.Setting.DTO.Enums;

namespace Alsahab.Setting.BL.Observers.ObserverStates
{
    public class BranchAdd : ObserverStateBase
    {
        public override LogActionType Type
        {
            get
            {
                return LogActionType.BranchAdd;
            }
        }
        public BranchDTO Branch { get; set; }
    }
    public class BranchEdit : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.BranchEdit;
            }
        }
        public BranchDTO Branch { get; set; }
    }
    public class BranchDelete : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.BranchDelete;
            }
        }
        public BranchDTO Branch { get; set; }
    }
}

