using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Gostar.Setting.DTO;
using Gostar.Common;

namespace Gostar.Setting.BL.Observers.ActionDTO
{
    public abstract class ActionBaseDTO
    {
        public UserInfoDTO User { get; set; }
        //public long GroupName { get; set; }
        //public long RegistererUserMemberID { get; set; }
        //public string RegistererUserMemberFullName { get; set; }
        //public string GroupMembersFullName { get; set; }
        //public List<long?> GroupMembersID { get; set; }
        public long GroupID { get; set; }
        public abstract DTO.Enums.SettingEntity Entity { get; }
        public long? RecordID { get; set; }
        public Gostar.Common.ActionType ActionType { get; set; }
        [JsonIgnore]
        public string MessageStr => JsonConvert.SerializeObject(this);
        [JsonIgnore]
        public abstract string DisplayMessage { get; }
        public static ActionBaseDTO CreateInstance(DTO.Enums.SettingEntity entity, string data)
        {
            switch (entity)
            {
                case DTO.Enums.SettingEntity.Area:
                    return JsonConvert.DeserializeObject<AreaActionDTO>(data);
                case DTO.Enums.SettingEntity.Branch:
                    return JsonConvert.DeserializeObject<BranchActionDTO>(data);
                case DTO.Enums.SettingEntity.BranchAddress:
                    return JsonConvert.DeserializeObject<BranchAddressActionDTO>(data);
                case DTO.Enums.SettingEntity.BranchRegionWork:
                    return JsonConvert.DeserializeObject<BranchRegionWorkActionDTO>(data);
                case DTO.Enums.SettingEntity.City:
                    return JsonConvert.DeserializeObject<CityActionDTO>(data);
                case DTO.Enums.SettingEntity.Country:
                    return JsonConvert.DeserializeObject<CountryActionDTO>(data);
                case DTO.Enums.SettingEntity.Currency:
                    return JsonConvert.DeserializeObject<CurrencyActionDTO>(data);
                case DTO.Enums.SettingEntity.ExchangeRate:
                    return JsonConvert.DeserializeObject<ExchangeRateActionDTO>(data);
                case DTO.Enums.SettingEntity.FormType:
                    return JsonConvert.DeserializeObject<FormTypeActionDTO>(data);
                case DTO.Enums.SettingEntity.GeneratedForm:
                    return JsonConvert.DeserializeObject<GeneratedFormActionDTO>(data);
                case DTO.Enums.SettingEntity.Prefix:
                    return JsonConvert.DeserializeObject<PrefixActionDTO>(data);
                case DTO.Enums.SettingEntity.Region:
                    return JsonConvert.DeserializeObject<RegionActionDTO>(data);
                case DTO.Enums.SettingEntity.RegionAgent:
                    return JsonConvert.DeserializeObject<RegionAgentActionDTO>(data);
                case DTO.Enums.SettingEntity.Rule:
                    return JsonConvert.DeserializeObject<RuleActionDTO>(data);
                case DTO.Enums.SettingEntity.RuleTag:
                    return JsonConvert.DeserializeObject<RuleTagActionDTO>(data);
                case DTO.Enums.SettingEntity.Sector:
                    return JsonConvert.DeserializeObject<SectorActionDTO>(data);
                case DTO.Enums.SettingEntity.Statement:
                    return JsonConvert.DeserializeObject<StatementActionDTO>(data);
                case Enums.SettingEntity.Subpart:
                    return JsonConvert.DeserializeObject<SubpartActionDTO>(data);
                case Enums.SettingEntity.Subsystem:
                    return JsonConvert.DeserializeObject<SubsystemActionDTO>(data);
                case Enums.SettingEntity.Typeoforganization:
                    return JsonConvert.DeserializeObject<TypeoforganizationActionDTO>(data);
                case Enums.SettingEntity.Zone:
                    return JsonConvert.DeserializeObject<ZoneActionDTO>(data);
            }
            return null;
        }
    }
}
