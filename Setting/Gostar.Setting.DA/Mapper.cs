using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;
using Gostar.Setting.DA.Entities;
using Gostar.Common;

namespace Gostar.Setting.DA
{
    public class Mapper
    {
        public static OrganizationalChart Map(OrganizationalChartDTO data)
        {
            return new OrganizationalChart
            {
                ID = data?.ID ?? 0,
                CreateDate = data?.CreateDate ?? DateTime.MinValue,
                IsDeleted = data?.IsDeleted ?? false,
                Title = data?.Title,
                ParentID = data?.ParentID,
                Code = data?.Code,
                Depth = data?.Depth,
                RightIndex = data?.RightIndex,
                LeftIndex = data?.LeftIndex,
                OldCode = data?.OldCode
            };
        }
        public static OrganizationalChartDTO Map(OrganizationalChart data)
        {
            return new OrganizationalChartDTO
            {
                ID = data?.ID ?? 0,
                CreateDate = data?.CreateDate,
                IsDeleted = data?.IsDeleted ?? false,
                Title = data?.Title,
                ParentID = data?.ParentID,
                ParentTitle = data?.OrganizationalChart2?.Title,
                Code = data?.Code,
                Depth = data?.Depth,
                RightIndex = data?.RightIndex,
                LeftIndex = data?.LeftIndex,
                OldCode = data?.OldCode
            };
        }

        public static Area Map(AreaDTO data)
        {
            return new Area
            {
                ID = data?.ID ?? 0,
                CityID = data?.CityID ?? 0,
                Name = data?.Name,
                Code = data?.Code ?? 0,
                CreateDate = data?.CreateDate ?? DateTime.MinValue,
                IsDeleted = data?.IsDeleted ?? false,

            };
        }
        public static AreaDTO Map(Area data)
        {
            return new AreaDTO
            {
                ID = data?.ID ?? 0,
                CityID = data?.CityID,
                Name = data?.Name,
                Code = data?.Code,
                CreateDate = data?.CreateDate,
                IsDeleted = data?.IsDeleted ?? false,
                CityName = data?.City.Name,
                CountryName = data?.City.Country.Name,
                CountryID = data?.City.Country.ID,
            };
        }

        public static Branch Map(BranchDTO data)
        {
            return new Branch
            {
                ID = data?.ID ?? 0,
                ParentID = data?.ParentID,
                Code = data?.Code,
                Title = data?.Title,
                HeadPersonID = data?.HeadPersonID ?? 0,
                BranchPhoneNo = data?.BranchPhoneNo,
                BranchEmail = data?.BranchEmail,
                BranchAddressID = data?.BranchAddressID ?? 0,
                Comment = data?.BranchComment,
                CreateDate = data?.CreateDate ?? DateTime.MinValue,
                IsCentral = data?.IsCentral ?? false,
                IsDeleted = data?.IsDeleted ?? false,
                RightIndex = data?.RightIndex,
                LeftIndex = data?.LeftIndex,
                Depth = data?.Depth,
                OldCode = data?.OldCode
            };
        }
        public static BranchDTO Map(Branch data)
        {
            return new BranchDTO
            {
                ID = data?.ID ?? 0,
                ParentID = data?.ParentID,
                Code = data?.Code,
                Title = data?.Title,
                HeadPersonID = data?.HeadPersonID ?? 0,
                BranchPhoneNo = data?.BranchPhoneNo,
                BranchEmail = data?.BranchEmail,
                BranchAddressID = data?.BranchAddressID,
                BranchComment = data?.Comment,
                CreateDate = data?.CreateDate ?? DateTime.MinValue,
                IsCentral = data?.IsCentral,
                IsDeleted = data?.IsDeleted ?? false,
                RightIndex = data?.RightIndex,
                LeftIndex = data?.LeftIndex,
                Depth = data?.Depth,
                OldCode = data?.OldCode
            };
        }

        public static BranchAddress Map(BranchAddressDTO data)
        {
            return new BranchAddress
            {
                ID = data?.ID ?? 0,
                ZoneID = data?.ZoneID ?? 0,
                Address = data?.Address,
                EndDate = data?.EndDate > DateTime.MinValue ? data.EndDate : null,
                StartDate = data?.StartDate > DateTime.MinValue ? data?.StartDate : null,
                CreateDate = data?.CreateDate ?? DateTime.MinValue,
                IsDeleted = data?.IsDeleted ?? false,
                Latitude = data?.Latitude,
                Longitude = data?.Longitude,

            };
        }
        public static BranchAddressDTO Map(BranchAddress data)
        {
            return new BranchAddressDTO
            {
                ID = data?.ID,
                ZoneID = data?.ZoneID,
                Address = data?.Address,
                EndDate = data?.EndDate,
                StartDate = data?.StartDate,
                CreateDate = data?.CreateDate,
                IsDeleted = data?.IsDeleted ?? false,
                Latitude = data?.Latitude,
                Longitude = data?.Longitude,
                ZoneDTO = Map(data.Zone)

            };
        }

        public static BranchRegionWork Map(BranchRegionWorkDTO data)
        {
            return new BranchRegionWork
            {
                ID = data?.ID ?? 0,
                BranchID = data?.BranchID ?? 0,
                ZoneID = data?.ZoneID ?? 0,
                CreateDate = data?.CreateDate ?? DateTime.MinValue,
                IsDeleted = data?.IsDeleted ?? false
            };
        }
        public static BranchRegionWorkDTO Map(BranchRegionWork data)
        {
            return new BranchRegionWorkDTO
            {
                ID = data?.ID ?? 0,
                BranchID = data?.BranchID ?? 0,
                ZoneID = data?.ZoneID,
                CreateDate = data?.CreateDate ?? DateTime.MinValue,
                IsDeleted = data?.IsDeleted ?? false
            };
        }

        public static City Map(CityDTO data)
        {
            return new City
            {

                ID = data?.ID ?? 0,
                Code = data?.Code ?? 0,
                CountryID = data?.CountryID ?? 0,
                CreateDate = data?.CreateDate ?? DateTime.MinValue,
                IsDeleted = data?.IsDeleted ?? false,
                Name = data?.Name,

            };
        }
        public static CityDTO Map(City data)
        {
            return new CityDTO
            {

                ID = data?.ID ?? 0,
                Code = data?.Code,
                CountryID = data?.CountryID ?? 0,
                CreateDate = data?.CreateDate ?? DateTime.MinValue,
                IsDeleted = data?.IsDeleted ?? false,
                Name = data?.Name,
                CountryName = data?.Country.Name,
                CountryPhoneCode = data?.Country.PhoneCode,

            };
        }

        public static Country Map(CountryDTO data)
        {
            return new Country
            {
                ID = data?.ID ?? 0,
                CreateDate = data?.CreateDate ?? DateTime.MinValue,
                IsDeleted = data?.IsDeleted ?? false,
                Name = data?.Name,
                PhoneCode = data?.PhoneCode,
                ShortName = data?.ShortName,
            };
        }
        public static CountryDTO Map(Country data)
        {
            return new CountryDTO
            {
                ID = data?.ID ?? 0,
                CreateDate = data?.CreateDate ?? DateTime.MinValue,
                IsDeleted = data?.IsDeleted ?? false,
                Name = data?.Name,
                PhoneCode = data?.PhoneCode,
                ShortName = data?.ShortName,
            };
        }

        public static Currency Map(CurrencyDTO data)
        {
            return new Currency
            {
                ID = data?.ID ?? 0,
                Title = data?.Title,
                Symbol = data?.Symbol,
                IsDeleted = data?.IsDeleted ?? false,
                CreateDate = data?.CreateDate ?? DateTime.MinValue
            };
        }
        public static CurrencyDTO Map(Currency data)
        {
            return new CurrencyDTO
            {
                ID = data.ID,
                Title = data.Title,
                Symbol = data.Symbol,
                IsDeleted = data.IsDeleted,
                CreateDate = data.CreateDate
            };
        }

        public static ExchangeRate Map(ExchangeRateDTO data)
        {
            return new ExchangeRate
            {
                ID = data?.ID ?? 0,
                FromCurrencyID = data?.FromCurrencyID ?? 0,
                ToCurrencyID = data?.ToCurrencyID ?? 0,
                Ratio = data?.Ratio ?? 0,
                Year = data?.Year ?? DateTime.MinValue,
                IsDeleted = data?.IsDeleted ?? false,
                CreateDate = data?.CreateDate ?? DateTime.MinValue
            };
        }
        public static ExchangeRateDTO Map(ExchangeRate data)
        {
            return new ExchangeRateDTO
            {
                ID = data.ID,
                FromCurrencyID = data.FromCurrencyID,
                ToCurrencyID = data.ToCurrencyID,
                FromCurrencyTitle = data.Currency.Title,
                FromCurrencySymbol = data.Currency.Symbol,
                ToCurrencyTitle = data.Currency1.Title,
                ToCurrencySymbol = data.Currency1.Symbol,
                Ratio = data.Ratio,
                Year = data.Year,
                CreateDate = data.CreateDate,
                IsDeleted = data.IsDeleted
            };
        }

        public static FormType Map(FormTypeDTO Data)
        {
            return new FormType
            {
                ID = Data?.ID ?? 0,
                PublicCode = Data?.PublicCode,
                EnumID = (int?)Data?.Enum,
                Title = Data?.Title,
                SubSystemID = Data?.SubSystemID ?? 0,
                Coment = Data?.Coment,
                IsDeleted = Data?.IsDeleted ?? false,
                CreateDate = Data?.CreateDate ?? DateTime.MinValue
            };
        }
        public static FormTypeDTO Map(FormType Data)
        {
            return new FormTypeDTO
            {
                ID = Data?.ID,
                Title = Data?.Title,
                PublicCode = Data?.PublicCode,
                Enum = Data?.EnumID != null ? (DTO.Enums.RequestType?)Data?.EnumID : null,
                SubSystemID = Data?.SubSystemID,
                SubSystemTitle = Data?.Subsystem?.Name,
                SubSystemShortName = Data?.Subsystem?.ShortName,
                IsDeleted = Data?.IsDeleted,
                Coment = Data?.Coment,
                CreateDate = Data?.CreateDate
            };
        }

        public static GeneratedForm Map(GeneratedFormDTO data)
        {
            return new GeneratedForm
            {
                ID = data?.ID ?? 0,
                PublicCode = data?.PublicCode,
                PrivateCode = data?.PrivateCode,
                SubsystemID = data?.SubSystemID ?? 0,
                UniqeCode = data?.UniqeCode ?? 0,
                CreateDate = data?.CreateDate ?? DateTime.MinValue,
                IsDeleted = data?.IsDeleted ?? false
            };
        }
        public static GeneratedFormDTO Map(GeneratedForm data)
        {
            return new GeneratedFormDTO
            {
                ID = data?.ID,
                PublicCode = data?.PublicCode,
                PrivateCode = data?.PrivateCode,
                SubSystemID = data?.SubsystemID,
                UniqeCode = data?.UniqeCode,
                CreateDate = data?.CreateDate,
                IsDeleted = data?.IsDeleted
            };
        }

        public static Log Map(Common.LogDTO data)
        {
            if (data == null)
                return null;

            return new Log
            {
                ID = data?.ID ?? 0,
                ActionTypeID = data?.ActionTypeID ?? 0,
                CreateDate = data?.CreateDate ?? DateTime.MinValue,
                EntityID = data?.EntityID ?? 0,
                UserID = data?.UserID ?? 0,
                RecordID = data?.RecordID ?? 0,
                Message = data?.MessageStr,
                GroupID = data?.GroupID,
                RegistrantPersonFullName = data?.RegistrantPersonFullName,
                RegistrantPersonID = data?.RegistrantPersonID,
                GroupName = data?.GroupName,
            };
        }
        public static Common.LogDTO Map(Log data)
        {
            return new Common.LogDTO
            {
                ID = data?.ID ?? 0,
                ActionTypeID = data?.ActionTypeID ?? 0,
                ActionTypeTitle = ((Gostar.Common.ActionType)data.ActionTypeID).GetDescription(),
                CreateDate = data?.CreateDate,
                EntityID = data?.EntityID ?? 0,
                EntityTitle = ((Enums.SettingEntity)data.EntityID).GetDescription(),
                UserID = data?.UserID ?? 0,
                RecordID = data?.RecordID ?? 0,
                MessageStr = data?.Message,
                GroupName = data?.GroupName,
                GroupID = data?.GroupID,
                RegistrantPersonFullName = data?.RegistrantPersonFullName,
                RegistrantPersonID = data?.RegistrantPersonID ?? 0,

            };
        }

        public static Prefix Map(PrefixDTO data)
        {
            return new Prefix
            {
                ID = data?.ID ?? 0,
                CreateDate = data?.CreateDate ?? DateTime.MinValue,
                Title = data?.Title,
                IsDeleted = data?.IsDeleted ?? false,
                IsDefault = data?.IsDefault ?? false
            };
        }
        public static PrefixDTO Map(Prefix data)
        {
            return new PrefixDTO
            {
                ID = data?.ID ?? 0,
                CreateDate = data?.CreateDate,
                IsDefault = data?.IsDefault,
                Title = data?.Title,
                IsDeleted = data?.IsDeleted,

            };
        }

        public static Region Map(RegionDTO data)
        {
            return new Region
            {
                ID = data?.ID ?? 0,
                AreaID = data?.AreaID ?? 0,
                Code = data?.Code ?? 0,
                CreateDate = data?.CreateDate ?? DateTime.MinValue,
                IsDeleted = data?.IsDeleted ?? false,
                Name = data?.Name,

            };
        }
        public static RegionDTO Map(Region data)
        {
            return new RegionDTO
            {
                ID = data?.ID ?? 0,
                AreaID = data?.AreaID ?? 0,
                Code = data?.Code,
                CreateDate = data?.CreateDate ?? DateTime.MinValue,
                IsDeleted = data?.IsDeleted ?? false,
                Name = data?.Name,
                AreaName = data?.Area.Name,
                CityID = data?.Area.City.ID,
                CityName = data?.Area.City.Name,
                CountryID = data?.Area.City.Country.ID,
                CountryName = data?.Area.City.Country.Name,
            };
        }

        public static RegionAgent Map(RegionAgentDTO data)
        {
            return new RegionAgent
            {
                ID = data?.ID ?? 0,
                CreateDate = data?.CreateDate ?? DateTime.MinValue,
                EndDate = data?.EndDate,
                IsDeleted = data?.IsDeleted ?? false,
                PersonID = data?.AgentPersonID ?? 0,
                RegionID = data?.RegionID ?? 0,
                StartDate = data?.StartDate ?? DateTime.MinValue,
            };
        }
        public static RegionAgentDTO Map(RegionAgent ra, Region r, Area a, City ci, Country co)
        {
            return new RegionAgentDTO
            {
                ID = ra?.ID ?? 0,
                CreateDate = ra?.CreateDate,
                EndDate = ra?.EndDate,
                IsDeleted = ra?.IsDeleted ?? false,
                AgentPersonID = ra?.PersonID ?? 0,
                RegionID = ra?.RegionID ?? 0,
                StartDate = ra?.StartDate,
                AreaID = a.ID,
                AreaName = a.Name,
                CityID = ci.ID,
                CityName = ci.Name,
                CountryID = co.ID,
                CountryName = co.Name,
                RegionName = r.Name,
                CityAreaRegionCode = ci.Code.ToString().PadLeft(2, '0') + a.Code.ToString().PadLeft(2, '0') + r.Code.ToString().PadLeft(2, '0'),
            };
        }

        public static Rule Map(RuleDTO data)
        {
            return new Rule
            {
                ID = data?.ID ?? 0,
                Title = data?.Title,
                Type = (int)data?.Type,
                Description = data?.Description,
                IsDeleted = data?.IsDeleted ?? false,
                CreateDate = data?.CreateDate ?? DateTime.MinValue
            };
        }
        public static RuleDTO Map(Rule data)
        {
            return new RuleDTO
            {
                ID = data?.ID,
                Title = data?.Title,
                Type = (RuleType)data?.Type,
                Description = data?.Description,
                CreateDate = data?.CreateDate,
                IsDeleted = data?.IsDeleted,

            };
        }

        public static RuleTag Map(RuleTagDTO data)
        {
            return new RuleTag
            {
                ID = data?.ID ?? 0,
                RuleID = data?.RuleID ?? 0,
                FormTypeID = data?.FormTypeID ?? 0,
                CreateDate = data?.CreateDate ?? DateTime.MinValue,
                IsDeleted = data?.IsDeleted ?? false
            };
        }
        public static RuleTagDTO Map(RuleTag data)
        {
            return new RuleTagDTO
            {
                ID = data?.ID,
                RuleID = data?.RuleID,
                FormTypeID = data?.FormTypeID,
                CreateDate = data?.CreateDate,
                IsDeleted = data?.IsDeleted,
                FormTypeTitle = data?.FormType?.Title,
                RuleDescription = data?.Rule?.Description,
                RuleType = (RuleType)data?.Rule?.Type,
                SubSystemID = data?.FormType?.SubSystemID,
                SubSystemTitle = data?.FormType?.Subsystem.Name
            };
        }

        public static Sector Map(SectorDTO data)
        {
            return new Sector
            {
                ID = data?.ID ?? 0,
                RegionID = data?.RegionID ?? 0,
                Name = data?.Name,
                Code = data?.Code,
                CreateDate = data?.CreateDate ?? DateTime.MinValue,
                IsDeleted = data?.IsDeleted ?? false
            };
        }
        public static SectorDTO Map(Sector data)
        {
            return new SectorDTO
            {
                ID = data?.ID,
                RegionID = data?.RegionID,
                Name = data?.Name,
                Code = data?.Code,
                CreateDate = data?.CreateDate,
                IsDeleted = data?.IsDeleted,

                RegionName = data?.Region?.Name,

                AreaID = data?.Region?.Area?.ID,
                AreaName = data?.Region?.Area?.Name,

                CityID = data?.Region?.Area?.City?.ID,
                CityName = data?.Region?.Area?.City?.Name,

                CountryID = data?.Region?.Area?.City?.Country?.ID,
                CountryName = data?.Region?.Area?.City?.Country?.Name

            };
        }

        public static Statement Map(DTO.StatementDTO data)
        {
            return new Statement
            {
                ID = data?.ID ?? 0,
                CreateDate = data?.CreateDate ?? DateTime.MinValue,
                IsDeleted = data?.IsDeleted ?? false,
                ArabicText = data?.ArabicText,
                EnglishText = data?.EnglishText,
                PersianText = data?.PersianText,
                //SubSystemID = data?.SubsystemID,
                TagName = data?.TagName,
                //TypeID = (int)data?.TypeID,
            };
        }
        public static DTO.StatementSubsystemDTO Map(Statement s, StatementSubsystem ss)
        {
            return new DTO.StatementSubsystemDTO
            {
                ID = (int)(s?.ID ?? 0),
                CreateDate = s?.CreateDate,
                IsDeleted = s?.IsDeleted ?? false,
                ArabicText = s?.ArabicText,
                EnglishText = s?.EnglishText,
                PersianText = s?.PersianText,
                SubsystemID = ss?.SubsystemID ?? 0,
                SubsystemName = ss?.Subsystem?.Name,// ss?.SubsystemID > 0 ? ((Enums.Subsystem)ss?.SubsystemID).ToString() : null,
                TagName = s?.TagName,
            };
        }

        public static StatementSubsystem Map(StatementSubsystemDTO data)
        {
            return new StatementSubsystem
            {
                ID = data?.ID ?? 0,
                StatementID = data?.StatementID ?? 0,
                SubsystemID = data?.SubsystemID ?? 0,
            };
        }
        public static StatementSubsystemDTO Map(StatementSubsystem data)
        {
            return new StatementSubsystemDTO
            {
                ID = data?.ID ?? 0,
                ArabicText = data?.Statement?.ArabicText,
                SubsystemID = data?.SubsystemID,
                EnglishText = data?.Statement?.EnglishText,
                PersianText = data?.Statement?.PersianText,
                StatementID = data?.StatementID,
                SubsystemName = data?.Subsystem?.Name,// ((Enums.Subsystem)data?.SubsystemID).ToString(),
                TagName = data?.Statement?.TagName,
                IsDeleted = data?.Statement?.IsDeleted,
            };
        }

        public static Subpart Map(SubpartDTO data)
        {
            return new Subpart
            {
                ID = data?.ID ?? 0,
                Name = data?.Name,
                CreateDate = data?.CreateDate ?? DateTime.MinValue,
                IsDeleted = data?.IsDeleted ?? false,
                Description = data?.Description,
                IsSystem = data?.IsSystem ?? false,
                IsActive = data?.IsActive ?? true,
                SubsystemID = data?.SubsystemID ?? 0,
            };
        }
        public static SubpartDTO Map(Subpart data)
        {
            return new SubpartDTO
            {
                ID = data?.ID ?? 0,
                Name = data?.Name,
                CreateDate = data?.CreateDate,
                IsDeleted = data?.IsDeleted ?? false,
                Description = data?.Description,
                IsActive = data?.IsActive,
                IsSystem = data?.IsSystem,
                SubsystemID = data?.SubsystemID,
                SubsystemName = data?.Subsystem?.Name,

            };
        }

        public static Subsystem Map(SubsystemDTO data)
        {
            return new Subsystem
            {
                ID = data?.ID ?? 0,
                Name = data?.Name,
                CreateDate = data?.CreateDate ?? DateTime.MinValue,
                ShortName = data?.ShortName,
                IsDeleted = data?.IsDeleted ?? false,
                Description = data?.Description,
                IsSystem = data?.IsSystem,
                IsActive = data?.IsActive,
                RunOrder = data?.RunOrder,
                IsPart = data?.IsPart ?? false,
            };
        }
        public static SubsystemDTO Map(Subsystem data)
        {
            return new SubsystemDTO
            {
                ID = data?.ID ?? 0,
                Name = data?.Name,
                ShortName = data?.ShortName,
                CreateDate = data?.CreateDate,
                IsDeleted = data?.IsDeleted ?? false,
                Description = data?.Description,
                IsActive = data?.IsActive,
                IsSystem = data.IsSystem,
                RunOrder = data?.RunOrder ?? 0,
                IsPart = data?.IsPart,
            };
        }

        public static Typeoforganization Map(TypeoforganizationDTO data)
        {
            return new Typeoforganization
            {
                ID = data?.ID ?? 0,
                CreateDate = data?.CreateDate ?? DateTime.MinValue,
                Title = data?.Title,
                IsDeleted = data?.IsDeleted ?? false,

            };
        }
        public static TypeoforganizationDTO Map(Typeoforganization data)
        {
            return new TypeoforganizationDTO
            {
                ID = data?.ID ?? 0,
                CreateDate = data?.CreateDate,
                Title = data?.Title,
                IsDeleted = data?.IsDeleted,

            };
        }

        public static Zone Map(ZoneDTO data)
        {
            return new Zone
            {
                ID = data?.ID ?? 0,
                Code = data?.Code,
                ParentID = data?.ParentID,
                Title = data?.Title,
                Type = (int)data?.Type,
                Comment = data?.Comment,
                LeftIndex = data?.LeftIndex,
                RightIndex = data?.RightIndex,
                Depth = data?.Depth,
                CreateDate = data?.CreateDate ?? DateTime.MinValue,
                IsDeleted = data?.IsDeleted ?? false,
                OldCode = data?.OldCode

            };
        }
        public static ZoneDTO Map(Zone data)
        {
            return new ZoneDTO
            {
                ID = data?.ID,
                Code = data?.Code,
                ParentID = data?.ParentID,
                Title = data?.Title,
                Type = (DTO.Enums.ZoneType)data?.Type,
                Comment = data?.Comment,
                CreateDate = data?.CreateDate,
                ParentTitle = data?.Zone2?.Title,
                LeftIndex = data?.LeftIndex,
                RightIndex = data?.RightIndex,
                Depth = data?.Depth,
                IsDeleted = data?.IsDeleted,
                OldCode = data?.OldCode
            };
        }

        private static List<long> MapStringToIDList(String s)
        {
            List<long> res = new List<long>();
            if (!String.IsNullOrWhiteSpace(s))
                res = s.Split(',')?.Select(long.Parse)?.ToList();
            return res;
        }
        private static String MapIDListToString(List<long> list)
        {
            if (list.Count > 0)
                return String.Join(",", list);
            else
                return "";
        }
    }
}
