using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;

namespace Gostar.Setting.BL.Observers.ObserverStates
{
    public class BranchAdd : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.BranchAdd;
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

