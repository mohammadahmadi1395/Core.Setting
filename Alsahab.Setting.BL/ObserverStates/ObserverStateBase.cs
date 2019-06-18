using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Common;
using static Alsahab.Setting.DTO.Enums;

namespace Alsahab.Setting.BL.Observers.ObserverStates
{
    public abstract class ObserverStateBase
    {
        public UserInfoDTO User { get; set; }
        public abstract LogActionType Type { get; }
    }
}
