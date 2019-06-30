using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Alsahab.Common;
using Alsahab.Setting.Data.Interfaces;
using Alsahab.Setting.Entities.Models;
using Gostar.Common;

namespace Alsahab.Setting.BL
{
    public class LogBL : BaseBL<Log, Alsahab.Common.LogDTO, Alsahab.Common.LogFilterDTO>
    {
        // private LogDA logDA;
        // private LogDA _LogDA => logDA ?? (logDA = new LogDA());
        private readonly IBaseDL<Log, Alsahab.Common.LogDTO, Alsahab.Common.LogFilterDTO> _LogDL;
        public LogBL(IBaseDL<Log, Alsahab.Common.LogDTO, Alsahab.Common.LogFilterDTO> logDL) : base(logDL)
        {
            _LogDL = logDL;
        }

        public async override Task<IList<Alsahab.Common.LogDTO>> GetAsync(Alsahab.Common.LogFilterDTO data, CancellationToken cancellationToken)
        {
            var result = await _LogDL.GetAsync(data, cancellationToken);
            if (result != null)
            {
                //TODO: این کارها باید در واسط کاربری انجام شوند یا اگر در سرویس انجام میشوند باید کش شوند
                var users = ServiceUtility.CallUserManagement(s => s.User(new UserManagement.SC.Messages.UserRequest
                {
                    ActionType = Gostar.Common.ActionType.Select,
                    //TODO: (Gostar.Common.UserInfoDTO) User,
                    User = new Gostar.Common.UserInfoDTO { UserID = 1 },
                    Filter = new UserManagement.DTO.UserFilterDTO
                    {
                        IDList = result?.Select(t => t?.UserID)?.ToList(),
                    }
                }))?.ResponseDtoList;

                var member = ServiceUtility.CallUserManagement(s => s.Member(new UserManagement.SC.Messages.MemberRequest
                {
                    ActionType = Gostar.Common.ActionType.Select,
                    //TODO:
                    User = new Gostar.Common.UserInfoDTO { UserID = 1 },
                    RequestDto = new UserManagement.DTO.MemberDTO
                    {
                        IDList = users?.Select(g => g.MemberID)?.ToList(),
                    }
                }))?.ResponseDtoList;


                var persons = ServiceUtility.CallMember(s => s.Person(new Alyatim.Member.SC.Messages.PersonRequest
                {
                    ActionType = Gostar.Common.ActionType.Select,
                    //TODO:
                    User = new Gostar.Common.UserInfoDTO { UserID = 1 },
                    PersonFilter = new Alyatim.Member.DTO.PersonFilterDTO
                    {
                        IDList = member?.Select(t => t.PersonID)?.ToList(),
                    }
                }))?.ResponseDtoList;
                result = (from r in result
                          join u in users on r.UserID equals u.ID
                          join m in member on u.MemberID equals m.ID
                          join p in persons on m.PersonID equals p.ID
                          select new Alsahab.Common.LogDTO
                          {
                              ActionTypeID = r.ActionTypeID,
                              EntityID = r.EntityID,
                              EntityTitle = r.EntityTitle,
                              CreateDate = r.CreateDate,
                              ActionTypeTitle = r.ActionTypeTitle,
                              GroupID = u.GroupID,
                              GroupName = u.GroupName,
                              RecordID = r.RecordID,
                              IsDeleted = r.IsDeleted,
                              MessageStr = r.MessageStr,
                              RegistrantPersonFullName = p.FullName,
                              RegistrantPersonID = p.ID,
                              UserID = u.ID ?? 0,
                              //TODO:
                              UserRoleType = ((Alsahab.Common.RoleType?)((int)(u.GroupRoleType))) ?? Alsahab.Common.RoleType.SingleUser,
                              BranchID = u.GroupBranchID,
                              BranchTitle = u.GroupBranchTitle,

                          })?.ToList();

                if (data?.BranchIDs?.Count > 0)
                    result = result?.Where(s => data.BranchIDs.Contains(s.BranchID ?? 0))?.ToList();
                if (!string.IsNullOrWhiteSpace(data?.FullName))
                    result = result?.Where(s => s.RegistrantPersonFullName.Contains(data.FullName ?? ""))?.ToList();
                if (data?.GroupIDs?.Count > 0)
                    result = result?.Where(s => data.GroupIDs.Contains(s.GroupID ?? 0))?.ToList();

                if (data?.UserRoleTypes?.Count > 0)
                    result = result?.Where(s => data.UserRoleTypes.Contains((int)s.UserRoleType))?.ToList();
                foreach (var d in result)
                {
                    //TODO: فعلا کامنت میشود تا خطاها مشخص شود
                    // try
                    // {
                        var obj = BL.Observers.ActionDTO.ActionBaseDTO.CreateInstance((Alsahab.Setting.DTO.Enums.SettingEntity)d.EntityID, d.MessageStr);
                        d.MessageStr = obj.DisplayMessage;
                    // }
                    // catch (Exception e)
                    // { }
                }
                if (!string.IsNullOrWhiteSpace(data?.Message))
                    result = result?.Where(s => s.MessageStr.Contains(data.Message ?? ""))?.ToList();
            }
            ResultCount = result.Count;

            if (PagingInfo?.IsPaging == true)
            {
                int skip = (PagingInfo.Index - 1) * PagingInfo.Size;
                result = result.OrderBy(s => s.ID).Skip(skip).Take(PagingInfo.Size).ToList();
            }

            ResponseStatus = _LogDL.ResponseStatus;
            if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
            {
                ErrorMessage += _LogDL.ErrorMessage;
                return null;
            }
            return result;
        }
    }
}
