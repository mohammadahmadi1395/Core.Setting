using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gostar.Setting.SC.Messages
{
    public class ZoneBranchResponse : BaseResponse<long>
    {
        List<long> ZoneIDList { get; set; }
    }
}
