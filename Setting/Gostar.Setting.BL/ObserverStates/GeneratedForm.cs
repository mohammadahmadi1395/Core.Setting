using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;

namespace Gostar.Setting.BL.Observers.ObserverStates
{
    public class GeneratedFormAdd : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.GeneratedFormAdd;
            }
        }
        public GeneratedFormDTO GeneratedForm { get; set; }
    }
    public class GeneratedFormEdit : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.GeneratedFormEdit;
            }
        }
        public GeneratedFormDTO GeneratedForm { get; set; }
    }
    public class GeneratedFormDelete : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.GeneratedFormDelete;
            }
        }
        public GeneratedFormDTO GeneratedForm { get; set; }
    }
}
