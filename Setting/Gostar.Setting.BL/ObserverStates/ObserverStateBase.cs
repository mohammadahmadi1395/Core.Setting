﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Common;

namespace Gostar.Setting.BL.Observers.ObserverStates
{
    public abstract class ObserverStateBase
    {
        public UserInfoDTO User { get; set; }
        public abstract DTO.Enums.LogActionType Type { get; }
    }
}
