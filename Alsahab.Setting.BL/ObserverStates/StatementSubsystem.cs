using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Setting.DTO;
using Alsahab.Common;
using Alsahab.Setting.DTO;

namespace Alsahab.Setting.BL.Observers.ObserverStates
{
    public class StatementSubsystemAdd : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.StatementSubsystemAdd;
            }
        }
        public StatementSubsystemDTO StatementSubsystem { get; set; }
    }
    public class StatementSubsystemEdit : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.StatementSubsystemEdit;
            }
        }
        public StatementSubsystemDTO StatementSubsystem { get; set; }
    }
    public class StatementSubsystemDelete : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.StatementSubsystemDelete;
            }
        }
        public StatementSubsystemDTO StatementSubsystem { get; set; }
    }
}
