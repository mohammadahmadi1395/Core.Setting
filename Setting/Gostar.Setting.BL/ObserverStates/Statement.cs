using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;
using Gostar.Common;
using Gostar.Setting.DTO;

namespace Gostar.Setting.BL.Observers.ObserverStates
{
    public class StatementAdd : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.StatementAdd;
            }
        }
        public DTO.StatementDTO Statement { get; set; }
    }
    public class StatementEdit : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.StatementEdit;
            }
        }
        public DTO.StatementDTO Statement { get; set; }
    }
    public class StatementDelete : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.StatementDelete;
            }
        }
        public DTO.StatementDTO Statement { get; set; }
    }
}
