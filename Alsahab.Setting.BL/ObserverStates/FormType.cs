using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Setting.DTO;

namespace Alsahab.Setting.BL.Observers.ObserverStates
{
    public class FormTypeAdd : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.FormTypeAdd;
            }
        }
        public FormTypeDTO FormType { get; set; }
    }
    public class FormTypeEdit : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.FormTypeEdit;
            }
        }
        public FormTypeDTO FormType { get; set; }
    }
    public class FormTypeDelete : ObserverStateBase
    {
        public override Enums.LogActionType Type
        {
            get
            {
                return Enums.LogActionType.FormTypeDelete;
            }
        }
        public FormTypeDTO FormType { get; set; }
    }
}
