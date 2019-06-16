using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Common;
using System.Runtime.Serialization;

namespace Gostar.Setting.SC.Messages
{
    public class StatementRequest : BaseRequest<DTO.StatementDTO>
    {
    }

    [KnownType(typeof(List<DTO.SubsystemDTO>))]
    [DataContract]
    public class StatementResponse : BaseResponse<DTO.StatementDTO>
    {
    }
}
