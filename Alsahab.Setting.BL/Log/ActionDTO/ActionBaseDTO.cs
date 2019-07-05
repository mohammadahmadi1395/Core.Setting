using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Alsahab.Common;
using Newtonsoft.Json;
using static Alsahab.Setting.DTO.Enums;

namespace Alsahab.Setting.BL.Log.ActionDTO
{
    public class ActionBaseDTO<Dto>
    where Dto : BaseDTO
    {
        public UserInfoDTO User { get; set; }
        public long GroupID { get; set; }
        public Dto DTO { get; set; }
        public SettingEntity Entity { get { return (SettingEntity)Enum.Parse(typeof(SettingEntity), typeof(Dto).Name.Substring(0, typeof(Dto).Name.Length - 3)); } }
        public long? RecordID { get; set; }
        public ActionType ActionType { get; set; }
        [JsonIgnore]
        public string MessageStr => JsonConvert.SerializeObject(this);
        [JsonIgnore]
        public virtual string DisplayMessage { get; set; }

        // public static ActionBaseDTO CreateInstance(SettingEntity entity, string data)
        // {
        //     switch (entity)
        //     {
        //         case SettingEntity.Branch:
        //             return JsonConvert.DeserializeObject<BranchActionDTO>(data);
        //         // case SettingEntity.BranchAddress:
        //         //     return JsonConvert.DeserializeObject<BranchAddressActionDTO>(data);
        //         // case SettingEntity.BranchRegionWork:
        //         //     return JsonConvert.DeserializeObject<BranchRegionWorkActionDTO>(data);
        //         // case SettingEntity.City:
        //         //     return JsonConvert.DeserializeObject<FormTypeActionDTO>(data);
        //         // case SettingEntity.GeneratedForm:
        //         //     return JsonConvert.DeserializeObject<GeneratedFormActionDTO>(data);
        //         // case SettingEntity.Prefix:
        //         //     return JsonConvert.DeserializeObject<PrefixActionDTO>(data);
        //         // case SettingEntity.Rule:
        //         //     return JsonConvert.DeserializeObject<RuleActionDTO>(data);
        //         // case SettingEntity.RuleTag:
        //         //     return JsonConvert.DeserializeObject<RuleTagActionDTO>(data);
        //         // case SettingEntity.Statement:
        //         //     return JsonConvert.DeserializeObject<StatementActionDTO>(data);
        //         // case SettingEntity.Subpart:
        //         //     return JsonConvert.DeserializeObject<SubpartActionDTO>(data);
        //         // case SettingEntity.Subsystem:
        //         //     return JsonConvert.DeserializeObject<SubsystemActionDTO>(data);
        //         // case SettingEntity.Typeoforganization:
        //         //     return JsonConvert.DeserializeObject<TypeoforganizationActionDTO>(data);
        //         // case SettingEntity.Zone:
        //         //     return JsonConvert.DeserializeObject<ZoneActionDTO>(data);
        //     }
        //     return null;
        // }
    }


    public class ActionBaseDTO
    {
        public UserInfoDTO User { get; set; }
        public long GroupID { get; set; }
        public Object DTO { get; set; }
        public string Entity { get; set; }
        public long? RecordID { get; set; }
        public ActionType ActionType { get; set; }
        [JsonIgnore]
        public string MessageStr => JsonConvert.SerializeObject(this);
        [JsonIgnore]
        public virtual string DisplayMessage { get; set; }
    }

}
//public long GroupName { get; set; }
//public long RegistererUserMemberID { get; set; }
//public string RegistererUserMemberFullName { get; set; }
//public string GroupMembersFullName { get; set; }
//public List<long?> GroupMembersID { get; set; }
