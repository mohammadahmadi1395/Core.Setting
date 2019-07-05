using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Configuration;
using Alsahab.Setting.BL.Log.ActionDTO;
using Alsahab.Setting.BL.Log;
using Alsahab.Common;
using static Alsahab.Setting.DTO.Enums;
using Alsahab.Setting.Data.Interfaces;
using Alsahab.Setting.Entities;
using Alsahab.Setting.Entities.Models;
using Alsahab.Setting.DTO;

namespace Alsahab.Setting.BL.Log
{
    internal class LogObserver<TDto> : ObserverBase<TDto>
    where TDto : BaseDTO
    {
        private readonly IBaseDL<Entities.Models.Log, LogDTO, LogFilterDTO> _LogDL;
        public LogObserver(IBaseDL<Entities.Models.Log, LogDTO, LogFilterDTO> logDL)
        {
            _LogDL = logDL;
        }

        // private List<Alyatim.Member.DTO.PersonDTO> PersonList { get; set; }
        //private UserManagement.DTO.AcUserDTO user { get; set; }
        //private UserManagement.DTO.AcUserDTO team { get; set; }

        protected override int DoNotify(ObserverStateBase<TDto> state)
        {
            if (!(state.User?.UserID > 0))
                return 0;

            var stateInfo = state;
            if (stateInfo == null) return 0;
            ActionBaseDTO<TDto> actionDto = new ActionBaseDTO<TDto>//Activator.CreateInstance(typeof(ActionBaseDTO), new Object[]
            {
                User = stateInfo.User,
                RecordID = stateInfo.DTO.ID,
                DTO = stateInfo.DTO,
                ActionType = stateInfo.Type,                
            };
            CreateLog(actionDto);
            return 1;
        }

        private void CreateLog(ActionBaseDTO<TDto> actionDto)
        {
            Alsahab.Common.LogDTO logDto = ConvertToLogDTO(actionDto);
            var response = _LogDL.Insert(logDto);
        }
        private LogDTO ConvertToLogDTO(ActionBaseDTO<TDto> actionDto)
        {
            // TODO
            // var person = ServiceUtility.CallMember(s => s.Person(new Alyatim.Member.SC.Messages.PersonRequest
            // {
            //     ActionType = Common.ActionType.Select,
            //     User = actionDto?.User,
            //     RequestID = actionDto?.User?.UserPersonID,
            // }))?.ResponseDto;

            var result = new LogDTO
            {
                UserID = actionDto?.User?.UserID ?? 0,
                RegistrantPersonID = actionDto?.User?.UserPersonID ?? 0,
                GroupName = actionDto?.User?.GroupName,
                UserRoleType = actionDto?.User?.UserRoleType ?? RoleType.SingleUser,
                // GroupMembersID = team?.ID > 0 ? team?.TeamMembers?.Select(s => s.MemberID)?.ToList() : null,
                // GroupMembersFullName = team?.ID > 0 ? string.Join(", ", MemberList?.Select(t => t.FullName)?.ToList()) : null,// team?.TeamMembers?.Select(s => s.MemberName)?.ToList())
                // RegistrantPersonFullName = person?.FullName,
                GroupID = actionDto?.User?.GroupID,
                ActionTypeID = (int)actionDto.ActionType,
                EntityID = (int)actionDto.Entity,
                Message = actionDto.MessageStr,
                RecordID = actionDto.DTO.ID ?? 0,
                CreateDate = DateTime.Now
            };
            return result;
        }

        //user = ServiceUtility.CallUserManagement(s => s.AcUserMember(new UserManagement.SC.Messages.AcUserMemberRequest
        //{
        //    UserID = state.UserID,
        //    ActionType = Common.ActionType.Select,
        //    RequestDto = new UserManagement.DTO.AcUserMemberDTO
        //    {
        //        UserID = state?.UserID,
        //        UserType = (long?)UserManagement.UserType.SingleUser,
        //    }
        //}))?.UserList?.FirstOrDefault();

        //if (!(user?.ID > 0))
        //    return 0;

        //team = ServiceUtility.CallUserManagement(s => s.AcUser(new UserManagement.SC.Messages.AcUserRequest
        //{
        //    UserID = state?.TeamID ?? 0,
        //    ActionType = Common.ActionType.Select,
        //    RequestDto = new UserManagement.DTO.AcUserDTO
        //    {
        //        ID = state?.TeamID,
        //    }
        //}))?.ResponseDtoList?.FirstOrDefault();

        //MemberList = ServiceUtility.CallMember(s => s.Member(new Alyatim.Member.SC.Messages.MemberRequest
        //{
        //    ActionType = Common.ActionType.Select,
        //    UserID = user?.ID ?? 0,
        //    MemberFilter = new Alyatim.Member.DTO.MemberFilterDTO
        //    {
        //        IDList = team?.ID > 0 ? team?.TeamMembers?.Select(t => t.MemberID)?.ToList() : new List<long?> { user?.SingleMemberID },
        //    }
        //}))?.ResponseDtoList;

        // int result = 0;
        //     switch (state.Type)
        //     {

        // case LogActionType.BranchAddressAdd:
        //     result = BranchAddressAdd(state);
        //     break;
        // case LogActionType.BranchAddressEdit:
        //     result = BranchAddressEdit(state);
        //     break;
        // case LogActionType.BranchAddressDelete:
        //     result = BranchAddressDelete(state);
        //     break;

        // case LogActionType.BranchRegionWorkAdd:
        //     result = BranchRegionWorkAdd(state);
        //     break;
        // case LogActionType.BranchRegionWorkEdit:
        //     result = BranchRegionWorkEdit(state);
        //     break;
        // case LogActionType.BranchRegionWorkDelete:
        //     result = BranchRegionWorkDelete(state);
        //     break;

        // case LogActionType.FormTypeAdd:
        //     result = FormTypeAdd(state);
        //     break;
        // case LogActionType.FormTypeEdit:
        //     result = FormTypeEdit(state);
        //     break;
        // case LogActionType.FormTypeDelete:
        //     result = FormTypeDelete(state);
        //     break;

        // case LogActionType.GeneratedFormAdd:
        //     result = GeneratedFormAdd(state);
        //     break;
        // case LogActionType.GeneratedFormEdit:
        //     result = GeneratedFormEdit(state);
        //     break;
        // case LogActionType.GeneratedFormDelete:
        //     result = GeneratedFormDelete(state);
        //     break;

        // case LogActionType.PrefixAdd:
        //     result = PrefixAdd(state);
        //     break;
        // case LogActionType.PrefixEdit:
        //     result = PrefixEdit(state);
        //     break;
        // case LogActionType.PrefixDelete:
        //     result = PrefixDelete(state);
        //     break;

        // case LogActionType.RuleAdd:
        //     result = RuleAdd(state);
        //     break;
        // case LogActionType.RuleEdit:
        //     result = RuleEdit(state);
        //     break;
        // case LogActionType.RuleDelete:
        //     result = RuleDelete(state);
        //     break;

        // case LogActionType.RuleTagAdd:
        //     result = RuleTagAdd(state);
        //     break;
        // case LogActionType.RuleTagEdit:
        //     result = RuleTagEdit(state);
        //     break;
        // case LogActionType.RuleTagDelete:
        //     result = RuleTagDelete(state);
        //     break;

        // case LogActionType.StatementAdd:
        //     result = StatementAdd(state);
        //     break;
        // case LogActionType.StatementEdit:
        //     result = StatementEdit(state);
        //     break;
        // case LogActionType.StatementDelete:
        //     result = StatementDelete(state);
        //     break;

        // case LogActionType.StatementSubsystemAdd:
        //     result = StatementSubsystemAdd(state);
        //     break;
        // case LogActionType.StatementSubsystemEdit:
        //     result = StatementSubsystemEdit(state);
        //     break;
        // case LogActionType.StatementSubsystemDelete:
        //     result = StatementSubsystemDelete(state);
        //     break;

        // case LogActionType.SubpartAdd:
        //     result = SubpartAdd(state);
        //     break;
        // case LogActionType.SubpartEdit:
        //     result = SubpartEdit(state);
        //     break;
        // case LogActionType.SubpartDelete:
        //     result = SubpartDelete(state);
        //     break;

        // case LogActionType.SubsystemAdd:
        //     result = SubsystemAdd(state);
        //     break;
        // case LogActionType.SubsystemEdit:
        //     result = SubsystemEdit(state);
        //     break;
        // case LogActionType.SubsystemDelete:
        //     result = SubsystemDelete(state);
        //     break;

        // case LogActionType.TypeoforganizationAdd:
        //     result = TypeoforganizationAdd(state);
        //     break;
        // case LogActionType.TypeoforganizationEdit:
        //     result = TypeoforganizationEdit(state);
        //     break;
        // case LogActionType.TypeoforganizationDelete:
        //     result = TypeoforganizationDelete(state);
        //     break;

        // case LogActionType.ZoneAdd:
        //     result = ZoneAdd(state);
        //     break;
        // case LogActionType.ZoneEdit:
        //     result = ZoneEdit(state);
        //     break;
        // case LogActionType.ZoneDelete:
        //     result = ZoneDelete(state);
        //     break;
        // }
        // return result;
        // }

        // private int BranchAdd(ObserverStateBase state)
        // {
        //     var stateInfo = state as BranchAdd;
        //     if (stateInfo == null) return 0;
        //     BranchActionDTO actionDto = new BranchActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.Branch.ID,
        //         Branch = stateInfo.Branch,
        //         ActionType = Alsahab.Common.ActionType.Insert,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }
        // private int BranchEdit(ObserverStateBase state)
        // {
        //     var stateInfo = state as BranchEdit;
        //     if (stateInfo == null) return 0;
        //     BranchActionDTO actionDto = new BranchActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.Branch.ID,
        //         Branch = stateInfo.Branch,
        //         ActionType = Alsahab.Common.ActionType.Update,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }
        // private int BranchDelete(ObserverStateBase state)
        // {
        //     var stateInfo = state as BranchDelete;
        //     if (stateInfo == null) return 0;
        //     BranchActionDTO actionDto = new BranchActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.Branch.ID,
        //         Branch = stateInfo.Branch,
        //         ActionType = Alsahab.Common.ActionType.Delete,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }

        // private int BranchAddressAdd(ObserverStateBase state)
        // {
        //     var stateInfo = state as BranchAddressAdd;
        //     if (stateInfo == null) return 0;
        //     BranchAddressActionDTO actionDto = new BranchAddressActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.BranchAddress.ID,
        //         BranchAddress = stateInfo.BranchAddress,
        //         ActionType = Alsahab.Common.ActionType.Insert,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }
        // private int BranchAddressEdit(ObserverStateBase state)
        // {
        //     var stateInfo = state as BranchAddressEdit;
        //     if (stateInfo == null) return 0;
        //     BranchAddressActionDTO actionDto = new BranchAddressActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.BranchAddress.ID,
        //         BranchAddress = stateInfo.BranchAddress,
        //         ActionType = Alsahab.Common.ActionType.Update,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }
        // private int BranchAddressDelete(ObserverStateBase state)
        // {
        //     var stateInfo = state as BranchAddressDelete;
        //     if (stateInfo == null) return 0;
        //     BranchAddressActionDTO actionDto = new BranchAddressActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.BranchAddress.ID,
        //         BranchAddress = stateInfo.BranchAddress,
        //         ActionType = Alsahab.Common.ActionType.Delete,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }

        // private int BranchRegionWorkAdd(ObserverStateBase state)
        // {
        //     var stateInfo = state as BranchRegionWorkAdd;
        //     if (stateInfo == null) return 0;
        //     BranchRegionWorkActionDTO actionDto = new BranchRegionWorkActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.BranchRegionWork.ID,
        //         BranchRegionWork = stateInfo.BranchRegionWork,
        //         ActionType = Alsahab.Common.ActionType.Insert,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }
        // private int BranchRegionWorkEdit(ObserverStateBase state)
        // {
        //     var stateInfo = state as BranchRegionWorkEdit;
        //     if (stateInfo == null) return 0;
        //     BranchRegionWorkActionDTO actionDto = new BranchRegionWorkActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.BranchRegionWork.ID,
        //         BranchRegionWork = stateInfo.BranchRegionWork,
        //         ActionType = Alsahab.Common.ActionType.Update,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }
        // private int BranchRegionWorkDelete(ObserverStateBase state)
        // {
        //     var stateInfo = state as BranchRegionWorkDelete;
        //     if (stateInfo == null) return 0;
        //     BranchRegionWorkActionDTO actionDto = new BranchRegionWorkActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.BranchRegionWork.ID,
        //         BranchRegionWork = stateInfo.BranchRegionWork,
        //         ActionType = Alsahab.Common.ActionType.Delete,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }

        // private int FormTypeAdd(ObserverStateBase state)
        // {
        //     var stateInfo = state as FormTypeAdd;
        //     if (stateInfo == null) return 0;
        //     FormTypeActionDTO actionDto = new FormTypeActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.FormType.ID,
        //         FormType = stateInfo.FormType,
        //         ActionType = Alsahab.Common.ActionType.Insert,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }
        // private int FormTypeEdit(ObserverStateBase state)
        // {
        //     var stateInfo = state as FormTypeEdit;
        //     if (stateInfo == null) return 0;
        //     FormTypeActionDTO actionDto = new FormTypeActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.FormType.ID,
        //         FormType = stateInfo.FormType,
        //         ActionType = Alsahab.Common.ActionType.Update,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }
        // private int FormTypeDelete(ObserverStateBase state)
        // {
        //     var stateInfo = state as FormTypeDelete;
        //     if (stateInfo == null) return 0;
        //     FormTypeActionDTO actionDto = new FormTypeActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.FormType.ID,
        //         FormType = stateInfo.FormType,
        //         ActionType = Alsahab.Common.ActionType.Delete,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }

        // private int GeneratedFormAdd(ObserverStateBase state)
        // {
        //     var stateInfo = state as GeneratedFormAdd;
        //     if (stateInfo == null) return 0;
        //     GeneratedFormActionDTO actionDto = new GeneratedFormActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.GeneratedForm.ID,
        //         GeneratedForm = stateInfo.GeneratedForm,
        //         ActionType = Alsahab.Common.ActionType.Insert,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }
        // private int GeneratedFormEdit(ObserverStateBase state)
        // {
        //     var stateInfo = state as GeneratedFormEdit;
        //     if (stateInfo == null) return 0;
        //     GeneratedFormActionDTO actionDto = new GeneratedFormActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.GeneratedForm.ID,
        //         GeneratedForm = stateInfo.GeneratedForm,
        //         ActionType = Alsahab.Common.ActionType.Update,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }
        // private int GeneratedFormDelete(ObserverStateBase state)
        // {
        //     var stateInfo = state as GeneratedFormDelete;
        //     if (stateInfo == null) return 0;
        //     GeneratedFormActionDTO actionDto = new GeneratedFormActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.GeneratedForm.ID,
        //         GeneratedForm = stateInfo.GeneratedForm,
        //         ActionType = Alsahab.Common.ActionType.Delete,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }

        // private int PrefixAdd(ObserverStateBase state)
        // {
        //     var stateInfo = state as PrefixAdd;
        //     if (stateInfo == null) return 0;
        //     PrefixActionDTO actionDto = new PrefixActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.Prefix.ID,
        //         Prefix = stateInfo.Prefix,
        //         ActionType = Alsahab.Common.ActionType.Insert,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }
        // private int PrefixEdit(ObserverStateBase state)
        // {
        //     var stateInfo = state as PrefixEdit;
        //     if (stateInfo == null) return 0;
        //     PrefixActionDTO actionDto = new PrefixActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.Prefix.ID,
        //         Prefix = stateInfo.Prefix,
        //         ActionType = Alsahab.Common.ActionType.Update,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }
        // private int PrefixDelete(ObserverStateBase state)
        // {
        //     var stateInfo = state as PrefixDelete;
        //     if (stateInfo == null) return 0;
        //     PrefixActionDTO actionDto = new PrefixActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.Prefix.ID,
        //         Prefix = stateInfo.Prefix,
        //         ActionType = Alsahab.Common.ActionType.Delete,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }

        // private int RuleAdd(ObserverStateBase state)
        // {
        //     var stateInfo = state as RuleAdd;
        //     if (stateInfo == null) return 0;
        //     RuleActionDTO actionDto = new RuleActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.Rule.ID,
        //         Rule = stateInfo.Rule,
        //         ActionType = Alsahab.Common.ActionType.Insert,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }
        // private int RuleEdit(ObserverStateBase state)
        // {
        //     var stateInfo = state as RuleEdit;
        //     if (stateInfo == null) return 0;
        //     RuleActionDTO actionDto = new RuleActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.Rule.ID,
        //         Rule = stateInfo.Rule,
        //         ActionType = Alsahab.Common.ActionType.Update,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }
        // private int RuleDelete(ObserverStateBase state)
        // {
        //     var stateInfo = state as RuleDelete;
        //     if (stateInfo == null) return 0;
        //     RuleActionDTO actionDto = new RuleActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.Rule.ID,
        //         Rule = stateInfo.Rule,
        //         ActionType = Alsahab.Common.ActionType.Delete,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }

        // private int RuleTagAdd(ObserverStateBase state)
        // {
        //     var stateInfo = state as RuleTagAdd;
        //     if (stateInfo == null) return 0;
        //     RuleTagActionDTO actionDto = new RuleTagActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.RuleTag.ID,
        //         RuleTag = stateInfo.RuleTag,
        //         ActionType = Alsahab.Common.ActionType.Insert,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }
        // private int RuleTagEdit(ObserverStateBase state)
        // {
        //     var stateInfo = state as RuleTagEdit;
        //     if (stateInfo == null) return 0;
        //     RuleTagActionDTO actionDto = new RuleTagActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.RuleTag.ID,
        //         RuleTag = stateInfo.RuleTag,
        //         ActionType = Alsahab.Common.ActionType.Update,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }
        // private int RuleTagDelete(ObserverStateBase state)
        // {
        //     var stateInfo = state as RuleTagDelete;
        //     if (stateInfo == null) return 0;
        //     RuleTagActionDTO actionDto = new RuleTagActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.RuleTag.ID,
        //         RuleTag = stateInfo.RuleTag,
        //         ActionType = Alsahab.Common.ActionType.Delete,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }

        // private int StatementAdd(ObserverStateBase state)
        // {
        //     var stateInfo = state as StatementAdd;
        //     if (stateInfo == null) return 0;
        //     StatementActionDTO actionDto = new StatementActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.Statement.ID,
        //         Statement = stateInfo.Statement,
        //         ActionType = Alsahab.Common.ActionType.Insert,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }
        // private int StatementEdit(ObserverStateBase state)
        // {
        //     var stateInfo = state as StatementEdit;
        //     if (stateInfo == null) return 0;
        //     StatementActionDTO actionDto = new StatementActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.Statement.ID,
        //         Statement = stateInfo.Statement,
        //         ActionType = Alsahab.Common.ActionType.Update,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }
        // private int StatementDelete(ObserverStateBase state)
        // {
        //     var stateInfo = state as StatementDelete;
        //     if (stateInfo == null) return 0;
        //     StatementActionDTO actionDto = new StatementActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.Statement.ID,
        //         Statement = stateInfo.Statement,
        //         ActionType = Alsahab.Common.ActionType.Delete,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }

        // private int StatementSubsystemAdd(ObserverStateBase state)
        // {
        //     var stateInfo = state as StatementSubsystemAdd;
        //     if (stateInfo == null) return 0;
        //     StatementSubsystemActionDTO actionDto = new StatementSubsystemActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.StatementSubsystem.ID,
        //         StatementSubsystem = stateInfo.StatementSubsystem,
        //         ActionType = Alsahab.Common.ActionType.Insert,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }
        // private int StatementSubsystemEdit(ObserverStateBase state)
        // {
        //     var stateInfo = state as StatementSubsystemEdit;
        //     if (stateInfo == null) return 0;
        //     StatementSubsystemActionDTO actionDto = new StatementSubsystemActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.StatementSubsystem.ID,
        //         StatementSubsystem = stateInfo.StatementSubsystem,
        //         ActionType = Alsahab.Common.ActionType.Update,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }
        // private int StatementSubsystemDelete(ObserverStateBase state)
        // {
        //     var stateInfo = state as StatementSubsystemDelete;
        //     if (stateInfo == null) return 0;
        //     StatementSubsystemActionDTO actionDto = new StatementSubsystemActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.StatementSubsystem.ID,
        //         StatementSubsystem = stateInfo.StatementSubsystem,
        //         ActionType = Alsahab.Common.ActionType.Delete,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }

        // private int SubpartAdd(ObserverStateBase state)
        // {
        //     var stateInfo = state as SubpartAdd;
        //     if (stateInfo == null) return 0;
        //     SubpartActionDTO actionDto = new SubpartActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.Subpart.ID,
        //         Subpart = stateInfo.Subpart,
        //         ActionType = Alsahab.Common.ActionType.Insert,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }
        // private int SubpartEdit(ObserverStateBase state)
        // {
        //     var stateInfo = state as SubpartEdit;
        //     if (stateInfo == null) return 0;
        //     SubpartActionDTO actionDto = new SubpartActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.Subpart.ID,
        //         Subpart = stateInfo.Subpart,
        //         ActionType = Alsahab.Common.ActionType.Update,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }
        // private int SubpartDelete(ObserverStateBase state)
        // {
        //     var stateInfo = state as SubpartDelete;
        //     if (stateInfo == null) return 0;
        //     SubpartActionDTO actionDto = new SubpartActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.Subpart.ID,
        //         Subpart = stateInfo.Subpart,
        //         ActionType = Alsahab.Common.ActionType.Delete,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }

        // private int SubsystemAdd(ObserverStateBase state)
        // {
        //     var stateInfo = state as SubsystemAdd;
        //     if (stateInfo == null) return 0;
        //     SubsystemActionDTO actionDto = new SubsystemActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.Subsystem.ID,
        //         Subsystem = stateInfo.Subsystem,
        //         ActionType = Alsahab.Common.ActionType.Insert,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }
        // private int SubsystemEdit(ObserverStateBase state)
        // {
        //     var stateInfo = state as SubsystemEdit;
        //     if (stateInfo == null) return 0;
        //     SubsystemActionDTO actionDto = new SubsystemActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.Subsystem.ID,
        //         Subsystem = stateInfo.Subsystem,
        //         ActionType = Alsahab.Common.ActionType.Update,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }
        // private int SubsystemDelete(ObserverStateBase state)
        // {
        //     var stateInfo = state as SubsystemDelete;
        //     if (stateInfo == null) return 0;
        //     SubsystemActionDTO actionDto = new SubsystemActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.Subsystem.ID,
        //         Subsystem = stateInfo.Subsystem,
        //         ActionType = Alsahab.Common.ActionType.Delete,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }

        // private int TypeoforganizationAdd(ObserverStateBase state)
        // {
        //     var stateInfo = state as TypeoforganizationAdd;
        //     if (stateInfo == null) return 0;
        //     TypeoforganizationActionDTO actionDto = new TypeoforganizationActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.Typeoforganization.ID,
        //         Typeoforganization = stateInfo.Typeoforganization,
        //         ActionType = Alsahab.Common.ActionType.Insert,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }
        // private int TypeoforganizationEdit(ObserverStateBase state)
        // {
        //     var stateInfo = state as TypeoforganizationEdit;
        //     if (stateInfo == null) return 0;
        //     TypeoforganizationActionDTO actionDto = new TypeoforganizationActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.Typeoforganization.ID,
        //         Typeoforganization = stateInfo.Typeoforganization,
        //         ActionType = Alsahab.Common.ActionType.Update,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }
        // private int TypeoforganizationDelete(ObserverStateBase state)
        // {
        //     var stateInfo = state as TypeoforganizationDelete;
        //     if (stateInfo == null) return 0;
        //     TypeoforganizationActionDTO actionDto = new TypeoforganizationActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.Typeoforganization.ID,
        //         Typeoforganization = stateInfo.Typeoforganization,
        //         ActionType = Alsahab.Common.ActionType.Delete,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }

        // private int ZoneAdd(ObserverStateBase state)
        // {
        //     var stateInfo = state as ZoneAdd;
        //     if (stateInfo == null) return 0;
        //     ZoneActionDTO actionDto = new ZoneActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.Zone.ID,
        //         Zone = stateInfo.Zone,
        //         ActionType = Alsahab.Common.ActionType.Insert,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }
        // private int ZoneEdit(ObserverStateBase state)
        // {
        //     var stateInfo = state as ZoneEdit;
        //     if (stateInfo == null) return 0;
        //     ZoneActionDTO actionDto = new ZoneActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.Zone.ID,
        //         Zone = stateInfo.Zone,
        //         ActionType = Alsahab.Common.ActionType.Update,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }
        // private int ZoneDelete(ObserverStateBase state)
        // {
        //     var stateInfo = state as ZoneDelete;
        //     if (stateInfo == null) return 0;
        //     ZoneActionDTO actionDto = new ZoneActionDTO
        //     {
        //         User = stateInfo.User,
        //         RecordID = stateInfo.Zone.ID,
        //         Zone = stateInfo.Zone,
        //         ActionType = Alsahab.Common.ActionType.Delete,
        //     };
        //     CreateLog(actionDto);
        //     return 1;
        // }

    }
}
