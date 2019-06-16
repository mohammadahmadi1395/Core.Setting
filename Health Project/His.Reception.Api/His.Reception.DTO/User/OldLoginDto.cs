using System;
using System.Collections.Generic;
using System.Text;

namespace His.Reception.DTO.User
{
    public class OldLoginDto
    {
        public int ActionType { get; set; }
        public RequestDto RequestDto { get; set; }
    }

    public class RequestDto {
        public string MemberUserName { get; set; }
        public string MemberPassword { get; set; }
        public int GroupRoleType { get; set; }
    }
}
