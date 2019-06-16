using His.Reception.DTO.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace His.Reception.DTO.User
{
    public class ResponseDto
    {
        public DateTime CreateDate { get; set; }
        public object FromCreateDate { get; set; }
        public int ID { get; set; }
        public bool IsDeleted { get; set; }
        public object ToCreateDate { get; set; }
        public List<int> BranchAccessList { get; set; }
        public int FaultLoginCount { get; set; }
        public int GroupBranchID { get; set; }
        public bool GroupBranchIsCentral { get; set; }
        public string GroupBranchTitle { get; set; }
        public int GroupID { get; set; }
        public bool GroupIsActive { get; set; }
        public bool GroupIsSystem { get; set; }
        public string GroupName { get; set; }
        public int GroupRoleType { get; set; }
        public object IsActive { get; set; }
        public DateTime LastFaultDate { get; set; }
        public object MacAddress { get; set; }
        public int MemberID { get; set; }
        public bool MemberIsActive { get; set; }
        public bool MemberIsMacAddress { get; set; }
        public string MemberPassword { get; set; }
        public int MemberPersonID { get; set; }
        public string MemberUserName { get; set; }
        public object PersonBranchID { get; set; }
        public object PersonBranchTitle { get; set; }
        public object PersonFatherName { get; set; }
        public object PersonGrandFatherName { get; set; }
        public object PersonMobile { get; set; }
        public object PersonName { get; set; }
    }

    public class LoginResponseDto : BaseResponseDto
    {
        public string Token { get; set; }
    }
}
