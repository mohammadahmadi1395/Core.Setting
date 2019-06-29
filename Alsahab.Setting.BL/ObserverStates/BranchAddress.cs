using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Setting.DTO;

namespace Alsahab.Setting.BL.Observers.ObserverStates
{
    public class BranchAddressAdd : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.BranchAddressAdd;
            }
        }
        public BranchAddressDTO BranchAddress { get; set; }
    }
    public class BranchAddressEdit : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.BranchAddressEdit;
            }
        }
        public BranchAddressDTO BranchAddress { get; set; }
    }
    public class BranchAddressDelete : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.BranchAddressDelete;
            }
        }
        public BranchAddressDTO BranchAddress { get; set; }
    }
}

