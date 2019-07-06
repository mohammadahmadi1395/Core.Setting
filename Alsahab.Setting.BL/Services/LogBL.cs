using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Alsahab.Common;
using Alsahab.Setting.BL.Log.ActionDTO;
using Alsahab.Setting.Data.Interfaces;
using Alsahab.Setting.Entities.Models;
using Gostar.Common;

namespace Alsahab.Setting.BL
{
    public class LogBL : BaseBL<Entities.Models.Log, Alsahab.Common.LogDTO, Alsahab.Common.LogFilterDTO>
    {
        // private LogDA logDA;
        // private LogDA _LogDA => logDA ?? (logDA = new LogDA());
        private readonly IBaseDL<Entities.Models.Log, Alsahab.Common.LogDTO, Alsahab.Common.LogFilterDTO> _LogDL;
        public LogBL(IBaseDL<Entities.Models.Log, Alsahab.Common.LogDTO, Alsahab.Common.LogFilterDTO> logDL) : base(logDL, logDL)
        {
            _LogDL = logDL;
        }

        public async override Task<IList<Alsahab.Common.LogDTO>> GetAsync(Alsahab.Common.LogFilterDTO data, CancellationToken cancellationToken, Alsahab.Common.PagingInfoDTO paging = null)
        {
            var result = await _LogDL.GetAsync(data, cancellationToken, paging);
            ResultCount = _LogDL.ResultCount;
            if (result != null)
            {
                //TODO: این کارها باید در واسط کاربری انجام شوند یا اگر در سرویس انجام میشوند باید کش شوند
                // var users = ServiceUtility.CallUserManagement(s => s.User(new UserManagement.SC.Messages.UserRequest
                // {
                //     ActionType = Gostar.Common.ActionType.Select,
                //     //TODO: (Gostar.Common.UserInfoDTO) User,
                //     User = new Gostar.Common.UserInfoDTO { UserID = 1 },
                //     Filter = new UserManagement.DTO.UserFilterDTO
                //     {
                //         IDList = result?.Select(actionDtoType => actionDtoType?.UserID)?.ToList(),
                //     }
                // }))?.ResponseDtoList;

                // var member = ServiceUtility.CallUserManagement(s => s.Member(new UserManagement.SC.Messages.MemberRequest
                // {
                //     ActionType = Gostar.Common.ActionType.Select,
                //     //TODO:
                //     User = new Gostar.Common.UserInfoDTO { UserID = 1 },
                //     RequestDto = new UserManagement.DTO.MemberDTO
                //     {
                //         IDList = users?.Select(g => g.MemberID)?.ToList(),
                //     }
                // }))?.ResponseDtoList;


                // var persons = ServiceUtility.CallMember(s => s.Person(new Alyatim.Member.SC.Messages.PersonRequest
                // {
                //     ActionType = Gostar.Common.ActionType.Select,
                //     //TODO:
                //     User = new Gostar.Common.UserInfoDTO { UserID = 1 },
                //     PersonFilter = new Alyatim.Member.DTO.PersonFilterDTO
                //     {
                //         IDList = member?.Select(actionDtoType => actionDtoType.PersonID)?.ToList(),
                //     }
                // }))?.ResponseDtoList;
                // //TODO:
                // result = (from r in result
                //           join u in users on r.UserID equals u.ID
                //           join m in member on u.MemberID equals m.ID
                //           join p in persons on m.PersonID equals p.ID
                //           select new Alsahab.Common.LogDTO
                //           {
                //               ActionTypeID = r.ActionTypeID,
                //               EntityID = r.EntityID,
                //               EntityTitle = ((Alsahab.Setting.DTO.Enums.SettingEntity)(r.EntityID)).GetDescription(), //r.EntityTitle,
                //               CreateDate = r.CreateDate,
                //               ActionTypeTitle = r.ActionTypeTitle,
                //               GroupID = u.GroupID,
                //               GroupName = u.GroupName,
                //               RecordID = r.RecordID,
                //               IsDeleted = r.IsDeleted,
                //               Message = r.Message,
                //               RegistrantPersonFullName = p.FullName,
                //               RegistrantPersonID = p.ID,
                //               UserID = u.ID ?? 0,
                //               //TODO:
                //               UserRoleType = ((Alsahab.Common.RoleType?)((int)(u.GroupRoleType))) ?? Alsahab.Common.RoleType.SingleUser,
                //               BranchID = u.GroupBranchID,
                //               BranchTitle = u.GroupBranchTitle,

                //           })?.ToList();

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
                    try
                    {
                        //TODO:
                        // زمانی که کامنتهای بالا برداشته شدند، این خط باید از اینجا حذف شود
                        d.EntityTitle = ((Alsahab.Setting.DTO.Enums.SettingEntity)(d.EntityID)).GetDescription();
                        // به دست آوردن نوع اکشن دی تی او
                        Type actionDtoType = Type.GetType("Alsahab.Setting.BL.Observers.ActionDTO." + d.EntityTitle + "ActionDTO");
                        // به دست آوردن نوع دی تی او
                        Type dtoType = Type.GetType("Alsahab.Setting.DTO." + d.EntityTitle + "DTO, " + typeof(DTO.Enums).Assembly);
                        // به دست آوردن کل محتوای اکشن دی تی او
                        var fullDtoMessage = Newtonsoft.Json.JsonConvert.DeserializeObject<ActionBaseDTO>(d.Message);
                        // به دست آوردن بخشی از محتوای اکشن دی تی او که شامل متن کامل مقدار دی تی او میشود
                        var c = Newtonsoft.Json.JsonConvert.DeserializeObject(fullDtoMessage.DTO.ToString(), dtoType);
                        // ساختن یک شیء از اکشن دی تی او که مقدار دی تی او را به آن پاس دهیم تا بعدا بتوانیم مقدار قابل نمایش را استخراج کنیم
                        var obj = Activator.CreateInstance(actionDtoType, new Object[] { }); //BL.Observers.ActionDTO.ActionBaseDTO<T>.CreateInstance((Alsahab.Setting.DTO.Enums.SettingEntity)d.EntityID, d.Message);
                        // با توجه به این که در شیء بالا مقدار دی تی او، خالی است ما مقدار دی تی او را به صورت زیر پر میکنیم
                        PropertyInfo prop = actionDtoType.GetProperty("DTO");
                        prop.SetValue(obj,c);
                        // به دست آوردن مقدار قابل نمایش از متن کامل
                        PropertyInfo p = actionDtoType.GetProperty("DisplayMessage");
                        d.Message = p.GetValue(obj).ToString();
                    }
                    catch (Exception e)
                    { }
                }
                if (!string.IsNullOrWhiteSpace(data?.Message))
                    result = result?.Where(s => s.Message.Contains(data.Message ?? ""))?.ToList();
            }
            // ResultCount = result.Count;

            // if (PagingInfo?.IsPaging == true)
            // {
            //     int skip = (PagingInfo.Index - 1) * PagingInfo.Size;
            //     result = result.OrderBy(s => s.ID).Skip(skip).Take(PagingInfo.Size).ToList();
            // }

            // ResponseStatus = _LogDL.ResponseStatus;
            // if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
            // {
            //     ErrorMessage += _LogDL.ErrorMessage;
            //     return null;
            // }
            return result;
        }
    }
}
