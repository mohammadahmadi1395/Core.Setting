using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Common;
using static Alsahab.Setting.DTO.Enums;

namespace Alsahab.Setting.BL.Log
{
    public class ObserverStateBase<TDto>
    where TDto : BaseDTO
    {
        public UserInfoDTO User { get; set; }
        public ActionType Type { get; set; }
        public TDto DTO { get; set; }
    }
}
