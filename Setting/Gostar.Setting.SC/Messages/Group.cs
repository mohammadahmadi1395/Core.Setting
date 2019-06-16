using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;
using UserManagement.DTO;
namespace Gostar.Setting.SC.Messages
{
    [DataContract]
    public class GroupRequest : BaseRequest<GroupDTO>
    {
       

    }

    [DataContract]
    public class GroupResponse : BaseResponse<GroupDTO>
    {
    }
}
