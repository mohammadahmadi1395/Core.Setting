// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using Gostar.Setting.DTO;
// using Gostar.Setting.DA;

// namespace Gostar.Setting.BL
// {
//     public class LogBL : BaseBusiness
//     {
//         private LogDA logDA;
//         private LogDA _LogDA => logDA ?? (logDA = new LogDA());
//         public List<Gostar.Common.LogDTO> LogGet(Gostar.Common.LogFilterDTO data)
//         {
//             List<Gostar.Common.LogDTO> result = _LogDA.LogGet(data);
//             if (result != null)
//             {
//                 var users = ServiceUtility.CallUserManagement(s => s.User(new UserManagement.SC.Messages.UserRequest
//                 {
//                     ActionType = Common.ActionType.Select,
//                     User = User,
//                     Filter = new UserManagement.DTO.UserFilterDTO
//                     {
//                         IDList = result?.Select(t => t?.UserID)?.ToList(),
//                     }
//                 }))?.ResponseDtoList;


//                 var member = ServiceUtility.CallUserManagement(s => s.Member(new UserManagement.SC.Messages.MemberRequest
//                 {
//                     ActionType = Common.ActionType.Select,
//                     User = User,
//                     RequestDto = new UserManagement.DTO.MemberDTO
//                     {
//                         IDList = users?.Select(g => g.MemberID)?.ToList(),
//                     }
//                 }))?.ResponseDtoList;


//                 var persons = ServiceUtility.CallMember(s => s.Person(new Alyatim.Member.SC.Messages.PersonRequest
//                 {
//                     ActionType = Common.ActionType.Select,
//                     User = User,
//                     PersonFilter = new Alyatim.Member.DTO.PersonFilterDTO
//                     {
//                         IDList = member?.Select(t => t.PersonID)?.ToList(),
//                     }
//                 }))?.ResponseDtoList;
//                 result = (from r in result
//                           join u in users on r.UserID equals u.ID
//                           join m in member on u.MemberID equals m.ID
//                           join p in persons on m.PersonID equals p.ID
//                           select new Gostar.Common.LogDTO
//                           {
//                               ActionTypeID = r.ActionTypeID,
//                               EntityID = r.EntityID,
//                               EntityTitle = r.EntityTitle,
//                               CreateDate = r.CreateDate,
//                               ActionTypeTitle = r.ActionTypeTitle,
//                               GroupID = u.GroupID,
//                               GroupName = u.GroupName,
//                               RecordID = r.RecordID,
//                               IsDeleted = r.IsDeleted,
//                               MessageStr = r.MessageStr,
//                               RegistrantPersonFullName = p.FullName,
//                               RegistrantPersonID = p.ID,
//                               UserID = u.ID ?? 0,
//                               UserRoleType = u.GroupRoleType ?? Common.RoleType.SingleUser,
//                               BranchID = u.GroupBranchID,
//                               BranchTitle = u.GroupBranchTitle,

//                           })?.ToList();

//                 if (data?.BranchIDs?.Count > 0)
//                     result = result?.Where(s => data.BranchIDs.Contains(s.BranchID ?? 0))?.ToList();
//                 if (!string.IsNullOrWhiteSpace(data?.FullName))
//                     result = result?.Where(s => s.RegistrantPersonFullName.Contains(data.FullName ?? ""))?.ToList();
//                 if (data?.GroupIDs?.Count > 0)
//                     result = result?.Where(s => data.GroupIDs.Contains(s.GroupID ?? 0))?.ToList();

//                 if (data?.UserRoleTypes?.Count > 0)
//                     result = result?.Where(s => data.UserRoleTypes.Contains((int)s.UserRoleType))?.ToList();
//                 foreach (var d in result)
//                 {
//                     try
//                     {
//                         var obj = BL.Observers.ActionDTO.ActionBaseDTO.CreateInstance((Enums.SettingEntity)d.EntityID, d.MessageStr);
//                         d.MessageStr = obj.DisplayMessage;
//                     }
//                     catch (Exception e)
//                     { }
//                 }
//                 if (!string.IsNullOrWhiteSpace(data?.Message))
//                     result = result?.Where(s => s.MessageStr.Contains(data.Message ?? ""))?.ToList();
//             }
//             ResultCount = result.Count;

//             if (PagingInfo != null)
//             {
//                 if (PagingInfo.IsPaging)
//                 {
//                     int skip = (PagingInfo.Index - 1) * PagingInfo.Size;
//                     result = result.OrderBy(s => s.ID).Skip(skip).Take(PagingInfo.Size).ToList();
//                 }
//             }
//             ResponseStatus = _LogDA.ResponseStatus;
//             if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
//             {
//                 ErrorMessage += _LogDA.ErrorMessage;
//                 return null;
//             }
//             return result;
//         }
//     }
// }
