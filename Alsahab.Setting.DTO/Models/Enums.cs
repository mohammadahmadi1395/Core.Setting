using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Alsahab.Setting.DTO
{
    public class Enums
    {
        
        [DataContract]

        public enum Question
        {
            [EnumMember]
            [Description("Are you sure you want to delete this item?")]
            Delete = 0,
        }

        [DataContract]
        public enum ErrorType
        {
            [EnumMember]
            [Description("Unknown user")]
            UnknownUserError = 0,
        }

        public enum ImportType
        {
            [Description("Multiple Add from Excel file")]
            AddFromExcelFile = 1,
            [Description("Single Add")]
            AddFromMembers = 2,
        }
        [DataContract]
        public enum SelectZoneType
        {
            [EnumMember]
            [Description("AllNode")]
            All = 0,
            [EnumMember]
            [Description("LastChild")]
            Child = 1

        }

        [DataContract]
        public enum ZoneType
        {
            [EnumMember]
            [Description("All")]
            All = 0,
            [EnumMember]
            [Description("کشور")]
            Country = 1,
            [EnumMember]
            [Description("شهر")]
            City = 2,
            [EnumMember]
            [Description("ناحیه")]
            Area = 3,
            [EnumMember]
            [Description("منظقه")]
            Region = 4,
            [EnumMember]
            [Description("بخش")]
            Sector = 5,
            [EnumMember]
            [Description("خیابان")]
            Street = 6,
            [EnumMember]
            [Description("کوچه")]
            Branch = 7,
        }

        [DataContract]
        public enum Tabs
        {
            [EnumMember]
            [Description("زیرسیستمها")]
            Subsystem = 0,
            [EnumMember]
            [Description("چندزبانگی")]
            MultiLanguage = 1,
            [EnumMember]
            [Description("مکانها")]
            Zone = 2,
            [EnumMember]
            [Description("ارز")]
            Currency = 3,
            [EnumMember]
            [Description("فرم ها")]
            Forms = 4,
            [EnumMember]
            [Description("فرم ها")]
            Other = 5,
            [EnumMember]
            [Description("گزارشها")]
            Report = 6,
        }

        [DataContract]
        public enum Panels
        {
            [EnumMember]
            [Description("تنظیمات زبانها")]
            MultiLanguages = 0,
            [EnumMember]
            [Description("مناطق")]
            Zones = 1,
            [EnumMember]
            [Description("زیرسیستمها")]
            Subsystems = 2,
            [EnumMember]
            [Description("ارز")]
            Currency = 3,
            [EnumMember]
            [Description("فرم ها")]
            Forms = 4,
            [EnumMember]
            [Description("فرم ها")]
            Other = 5,
            [EnumMember]
            [Description("گزارشها")]
            Reports = 6,
        }

        [DataContract]
        public enum Buttons
        {
            [EnumMember]
            [Description("چندزبانی")]
            MultiLanguage = 0,
            [EnumMember]
            [Description("کشور")]
            Country = 1,
            [EnumMember]
            [Description("شهر")]
            City = 2,
            [EnumMember]
            [Description("منطقه")]
            Area = 3,
            [EnumMember]
            [Description("ناحیه")]
            Region = 4,
            [EnumMember]
            [Description("مسئول ناحیه")]
            RegionAgent = 5,
            [EnumMember]
            [Description("شعبه ها")]
            Branch = 6,
            [EnumMember]
            [Description("زیرسیستمها")]
            Subsystem = 7,
            [EnumMember]
            [Description("گزارش عملیات کاربران")]
            Log = 8,
            [EnumMember]
            [Description("Subpart")]
            Subpart = 9,

            [EnumMember]
            [Description("واحد پول")]
            CurrencyType = 10,

            [EnumMember]
            [Description("نوع فرم")]
            FormType = 11,

            [EnumMember]
            [Description("قوانین")]
            Rules = 12,

            [EnumMember]
            [Description("نوع سازمان")]
            OrganizationType = 13,

            [EnumMember]
            [Description("شهرت")]
            Prefix = 14,

            [EnumMember]
            [Description("نرخ تبدیل")]
            ExchangeRate = 15,

            [EnumMember]
            [Description("نرخ تبدیل")]
            Zone = 16,
            OrganizationalChar = 17,
        }

        //[DataContract]
        //public enum StatementTypes
        //{
        //    [EnumMember]
        //    [Description("Nothing")]
        //    Nothing = 0,
        //    [EnumMember]
        //    [Description("Control")]
        //    Control = 1,
        //    [EnumMember]
        //    [Description("Message")]
        //    Message = 2,
        //}

        [DataContract]
        public enum SettingEntity
        {
            [EnumMember]
            [Description("All")]
            All = 0,

            [EnumMember]
            [Description("Area")]
            Area = 1,

            [EnumMember]
            [Description("Branch")]
            Branch = 2,

            [EnumMember]
            [Description("BranchAddress")]
            BranchAddress = 3,

            [EnumMember]
            [Description("BranchRegionWork")]
            BranchRegionWork = 4,

            [EnumMember]
            [Description("City")]
            City = 5,

            [EnumMember]
            [Description("Country")]
            Country = 6,

            [EnumMember]
            [Description("Currency")]
            Currency = 7,

            [EnumMember]
            [Description("ExchangeRate")]
            ExchangeRate = 8,

            [EnumMember]
            [Description("FormType")]
            FormType = 9,

            [EnumMember]
            [Description("GeneratedForm")]
            GeneratedForm = 10,

            [EnumMember]
            [Description("Log")]
            Log = 11,

            [EnumMember]
            [Description("Prefix")]
            Prefix = 12,

            [EnumMember]
            [Description("Region")]
            Region = 13,

            [EnumMember]
            [Description("RegionAgent")]
            RegionAgent = 14,

            [EnumMember]
            [Description("Rule")]
            Rule = 15,

            [EnumMember]
            [Description("RuleTag")]
            RuleTag = 16,

            [EnumMember]
            [Description("Sector")]
            Sector = 17,

            [EnumMember]
            [Description("Statement")]
            Statement = 18,

            [EnumMember]
            [Description("StatementSubsystem")]
            StatementSubsystem = 19,

            [EnumMember]
            [Description("SubPart")]
            Subpart = 20,

            [EnumMember]
            [Description("Subsystem")]
            Subsystem = 21,

            [EnumMember]
            [Description("Typeoforganization")]
            Typeoforganization = 22,

            [EnumMember]
            [Description("Zone")]
            Zone = 23,
            [EnumMember]
            [Description("OrganizationalChart")]
            OrganizationalChart = 24,
        }

        [DataContract]
        public enum LogActionType
        {
            [EnumMember]
            [Description("همه موارد")]
            All = 0,

            [EnumMember]
            [Description("افزودن ناحیه")]
            AreaAdd = 1,
            [EnumMember]
            [Description("ویرایش ناحیه")]
            AreaEdit = 2,
            [EnumMember]
            [Description("حذف ناحیه")]
            AreaDelete = 3,

            [EnumMember]
            [Description("افزودن شعبه")]
            BranchAdd = 4,
            [EnumMember]
            [Description("ویرایش شعبه")]
            BranchEdit = 5,
            [EnumMember]
            [Description("حذف شعبه")]
            BranchDelete = 6,

            [EnumMember]
            [Description("افزودن آدرس شعبه")]
            BranchAddressAdd = 7,
            [EnumMember]
            [Description("ویرایش آدرس شعبه")]
            BranchAddressEdit = 8,
            [EnumMember]
            [Description("حذف آدرس شعبه")]
            BranchAddressDelete = 9,

            [EnumMember]
            [Description("افزودن ناحیه شعبه")]
            BranchRegionWorkAdd = 10,
            [EnumMember]
            [Description("ویرایش ناحیه شعبه")]
            BranchRegionWorkEdit = 11,
            [EnumMember]
            [Description("حذف ناحیه شعبه")]
            BranchRegionWorkDelete = 12,

            [EnumMember]
            [Description("افزودن شهر")]
            CityAdd = 13,
            [EnumMember]
            [Description("ویرایش شهر")]
            CityEdit = 14,
            [EnumMember]
            [Description("حذف شهر")]
            CityDelete = 15,

            [EnumMember]
            [Description("افزودن کشور")]
            CountryAdd = 16,
            [EnumMember]
            [Description("ویرایش کشور")]
            CountryEdit = 17,
            [EnumMember]
            [Description("حذف کشور")]
            CountryDelete = 18,

            [EnumMember]
            [Description("افزودن ارز")]
            CurrencyAdd = 19,
            [EnumMember]
            [Description("ویرایش ارز")]
            CurrencyEdit = 20,
            [EnumMember]
            [Description("حذف ارز")]
            CurrencyDelete = 21,

            [EnumMember]
            [Description("افزودن نرخ تبادلات")]
            ExchangeRateAdd = 22,
            [EnumMember]
            [Description("ویرایش نرخ تبادلات")]
            ExchangeRateEdit = 23,
            [EnumMember]
            [Description("حذف نرخ تبادلات")]
            ExchangeRateDelete = 24,

            [EnumMember]
            [Description("افزودن نوع فرم")]
            FormTypeAdd = 25,
            [EnumMember]
            [Description("ویرایش نوع فرم")]
            FormTypeEdit = 26,
            [EnumMember]
            [Description("حذف نوع فرم")]
            FormTypeDelete = 27,

            [EnumMember]
            [Description("افزودن فرم تولیدشده")]
            GeneratedFormAdd = 28,
            [EnumMember]
            [Description("ویرایش فرم تولیدشده")]
            GeneratedFormEdit = 29,
            [EnumMember]
            [Description("حذف فرم تولیدشده")]
            GeneratedFormDelete = 30,

            [EnumMember]
            [Description("افزودن پیشوند")]
            PrefixAdd = 31,
            [EnumMember]
            [Description("ویرایش پیشوند")]
            PrefixEdit = 32,
            [EnumMember]
            [Description("حذف پیشوند")]
            PrefixDelete = 33,

            [EnumMember]
            [Description("افزودن منطقه")]
            RegionAdd = 34,
            [EnumMember]
            [Description("ویرایش منطقه")]
            RegionEdit = 35,
            [EnumMember]
            [Description("حذف منطقه")]
            RegionDelete = 36,

            [EnumMember]
            [Description("افزودن مسئول ناحیه")]
            RegionAgentAdd = 37,
            [EnumMember]
            [Description("ویرایش مسئول ناحیه")]
            RegionAgentEdit = 38,
            [EnumMember]
            [Description("حذف مسئول ناحیه")]
            RegionAgentDelete = 39,

            [EnumMember]
            [Description("افزودن نقش")]
            RuleAdd = 40,
            [EnumMember]
            [Description("ویرایش نقش")]
            RuleEdit = 41,
            [EnumMember]
            [Description("حذف نقش")]
            RuleDelete = 42,

            [EnumMember]
            [Description("افزودن برچسب نقش")]
            RuleTagAdd = 43,
            [EnumMember]
            [Description("ویرایش برچسب نقش")]
            RuleTagEdit = 44,
            [EnumMember]
            [Description("حذف برچسب نقش")]
            RuleTagDelete = 45,

            [EnumMember]
            [Description("افزودن بخش")]
            SectorAdd = 46,
            [EnumMember]
            [Description("ویرایش بخش")]
            SectorEdit = 47,
            [EnumMember]
            [Description("حذف بخش")]
            SectorDelete = 48,

            [EnumMember]
            [Description("افزودن عبارت")]
            StatementAdd = 49,
            [EnumMember]
            [Description("ویرایش عبارت")]
            StatementEdit = 50,
            [EnumMember]
            [Description("حذف عبارت")]
            StatementDelete = 51,

            [EnumMember]
            [Description("افزودن عبارت")]
            StatementSubsystemAdd = 52,
            [EnumMember]
            [Description("ویرایش عبارت")]
            StatementSubsystemEdit = 53,
            [EnumMember]
            [Description("حذف عبارت")]
            StatementSubsystemDelete = 54,

            [EnumMember]
            [Description("SubpartAdd")]
            SubpartAdd = 55,
            [EnumMember]
            [Description("SubpartEdit")]
            SubpartEdit = 56,
            [EnumMember]
            [Description("SubpartDelete")]
            SubpartDelete = 57,

            [EnumMember]
            [Description("افزودن زیرسیسیتم")]
            SubsystemAdd = 58,
            [EnumMember]
            [Description("ویرایش زیرسیستم")]
            SubsystemEdit = 59,
            [EnumMember]
            [Description("حذف زیرسیستم")]
            SubsystemDelete = 60,

            [EnumMember]
            [Description("افزودن نوع سازمان")]
            TypeoforganizationAdd = 61,
            [EnumMember]
            [Description("ویرایش نوع سازمان")]
            TypeoforganizationEdit = 62,
            [EnumMember]
            [Description("حذف نوع سازمان")]
            TypeoforganizationDelete = 63,

            [EnumMember]
            [Description("ZoneAdd")]
            ZoneAdd = 64,
            [EnumMember]
            [Description("ZoneEdit")]
            ZoneEdit = 65,
            [EnumMember]
            [Description("ZoneDelete")]
            ZoneDelete = 66,
        }

        [DataContract]
        public enum RequestType
        {
            [EnumMember]
            [Description("جميع")]
            All = 0,

            [EnumMember]
            [Description("طلب العضوية")]
            Fa_MembershipRequest = 1,

            [EnumMember]
            [Description("طلب تسجيل الكفالة")]
            Sp_SaveSponsorship = 2,

            [EnumMember]
            [Description("طلب سداد الكفالة")]
            Sp_EditSponsorship = 3,

            [EnumMember]
            [Description("طلب لإلغاء الكفالة")]
            Sp_DeleteSponsorship = 4,

            [EnumMember]
            [Description("استخدام")]
            HR_Employee = 5,

            [EnumMember]
            [Description("طلب العضوية")]
            Bo_MembershipRequest = 6,

            [EnumMember]
            [Description("انصراف")]
            HR_quit = 7,



        }
        public enum SelectTreeType
        {
            [EnumMember]
            [Description("AllNode")]
            All = 0,
            [EnumMember]
            [Description("LastChild")]
            Child = 1
        }
        //[DataContract]
        //public enum Subsystem
        //{
        //    [EnumMember]
        //    [Description("هیچکدام")]
        //    Nothing = 0,
        //    [EnumMember]
        //    [Description("تشریفات")]
        //    InQuery = 1,
        //    [EnumMember]
        //    [Description("اعضا")]
        //    Member = 2,
        //    [EnumMember]
        //    [Description("یتیم و فقیر")]
        //    OrphanPoor = 3,
        //    [EnumMember]
        //    [Description("مدیریت فایل")]
        //    FileManagement = 4,
        //    [EnumMember]
        //    [Description("واسط کاربری مجتمع")]
        //    IntegratedUI = 5,
        //    [EnumMember]
        //    [Description("مدیریت کاربران")]
        //    UserManagement = 6,
        //    [EnumMember]
        //    [Description("Setting")]
        //    Setting = 7,
        //}

        //public enum Subpart
        //{
        //    [EnumMember]
        //    [Description("هیچکدام")]
        //    Nothing = 0,
        //    [EnumMember]
        //    [Description("Countries")]
        //    Country = 1,
        //    [EnumMember]
        //    [Description("Cities")]
        //    City = 2,
        //    [EnumMember]
        //    [Description("Areas")]
        //    Area = 3,
        //    [EnumMember]
        //    [Description("Regions")]
        //    Region = 4,
        //    [EnumMember]
        //    [Description("RegionAgents")]
        //    RegionAgent = 5,
        //    [EnumMember]
        //    [Description("Branchs")]
        //    Branch = 54,
        //    [EnumMember]
        //    [Description("Statements")]
        //    Statement = 6,
        //    [EnumMember]
        //    [Description("Log")]
        //    Log = 53,
        //    [EnumMember]
        //    [Description("PersonalInfo")]
        //    PersonalInfo = 7,
        //    [EnumMember]
        //    [Description("PersonAddress")]
        //    PersonAddress = 8,
        //    [EnumMember]
        //    [Description("PersonIllness")]
        //    PersonIllness = 9,
        //    [EnumMember]
        //    [Description("PersonInterest")]
        //    PersonInterest = 10,
        //    [EnumMember]
        //    [Description("PersonJob")]
        //    PersonJob = 11,
        //    [EnumMember]
        //    [Description("Member")]
        //    Member = 12,
        //    [EnumMember]
        //    [Description("Job")]
        //    Job = 13,
        //    [EnumMember]
        //    [Description("InterestSubject")]
        //    InterestSubject = 14,
        //    [EnumMember]
        //    [Description("IllnessSubject")]
        //    IllnessSubject = 15,
        //    [EnumMember]
        //    [Description("PersonHealthStatus")]
        //    PersonHealthStatus = 16,
        //    [EnumMember]
        //    [Description("DeathReason")]
        //    DeathReason = 17,
        //    [EnumMember]
        //    [Description("PersonDeathDetail")]
        //    PersonDeathDetail = 18,
        //    [EnumMember]
        //    [Description("EducationLeaveReason")]
        //    EducationLeaveReason = 20,
        //    [EnumMember]
        //    [Description("Lesson")]
        //    Lesson = 32,
        //    [EnumMember]
        //    [Description("EducationalGrade")]
        //    EducationalGrade = 55,
        //    [EnumMember]
        //    [Description("FamilyGroup")]
        //    FamilyGroup = 56,
        //    [EnumMember]
        //    [Description("StudyField")]
        //    StudyField = 58,
        //    [EnumMember]
        //    [Description("Tag")]
        //    Tag = 59,
        //    [EnumMember]
        //    [Description("PersonEducationalGrade")]
        //    PersonEducationalGrade = 34,
        //    [EnumMember]
        //    [Description("PersonEducationalStatus")]
        //    PersonEducationalStatus = 35,
        //    [EnumMember]
        //    [Description("PersonEducationLeave")]
        //    PersonEducationLeave = 36,
        //    [EnumMember]
        //    [Description("PersonLessonStatus")]
        //    PersonLessonStatus = 37,
        //    [EnumMember]
        //    [Description("PersonProblem")]
        //    PersonProblem = 38,
        //    [EnumMember]
        //    [Description("PersonProblemType")]
        //    PersonProblemType = 39,
        //    //[EnumMember]
        //    //[Description("OrphanEducationalGrade")]
        //    //OrphanPoor_OrphanEducationalGrade = 34,
        //    //[EnumMember]
        //    //[Description("OrphanEducationalStatus")]
        //    //OrphanPoor_OrphanEducationalStatus = 35,
        //    //[EnumMember]
        //    //[Description("OrphanLeaveEducation")]
        //    //OrphanPoor_OrphanLeaveEducation = 36,
        //    //[EnumMember]
        //    //[Description("OrphanLessonStatus")]
        //    //OrphanPoor_OrphanLessonStatus = 37,
        //    //[EnumMember]
        //    //[Description("OrphanProblem")]
        //    //OrphanPoor_OrphanProblem = 38,
        //    //[EnumMember]
        //    //[Description("OrphanProblemType")]
        //    //OrphanPoor_OrphanProblemType = 39,

        //    [EnumMember]
        //    [Description("Asset")]
        //    Asset = 19,
        //    [EnumMember]
        //    [Description("Family")]
        //    Family = 21,
        //    [EnumMember]
        //    [Description("FamilyAddress")]
        //    FamilyAddress = 22,
        //    [EnumMember]
        //    [Description("FamilyAsset")]
        //    FamilyAsset = 23,
        //    [EnumMember]
        //    [Description("FamilyEconomicStatus")]
        //    FamilyEconomicStatus = 24,
        //    [EnumMember]
        //    [Description("FamilyRelative")]
        //    FamilyRelative = 25,
        //    [EnumMember]
        //    [Description("Foundation")]
        //    Foundation = 26,
        //    [EnumMember]
        //    [Description("FoundationAssistance")]
        //    FamilyFoundationAssistance = 27,
        //    [EnumMember]
        //    [Description("FoundationAssistantType")]
        //    FoundationAssistantType = 28,
        //    [EnumMember]
        //    [Description("HouseBuildingType")]
        //    HouseBuildingType = 29,
        //    [EnumMember]
        //    [Description("HouseOwnershipType")]
        //    HouseOwnershipType = 30,
        //    [EnumMember]
        //    [Description("HousingStatus")]
        //    HousingStatus = 31,
        //    [EnumMember]
        //    [Description("Orphan")]
        //    Orphan = 33,
        //    [EnumMember]
        //    [Description("FamilyFiles")]
        //    FamilyFiles = 63,

        //    [EnumMember]
        //    [Description("ManageRequests")]
        //    ManageRequests = 40,
        //    [EnumMember]
        //    [Description("HandleRequests")]
        //    HandleRequests = 41,

        //    [EnumMember]
        //    [Description("PermissionType")]
        //    PermissionType = 43,
        //    [EnumMember]
        //    [Description("Group")]
        //    Group = 44,
        //    [EnumMember]
        //    [Description("User")]
        //    User = 45,
        //    [EnumMember]
        //    [Description("Subsystem")]
        //    Subystem = 46,
        //    [EnumMember]
        //    [Description("Subpart")]
        //    Subpart = 47,
        //    [EnumMember]
        //    [Description("Role")]
        //    Role = 48,
        //    [EnumMember]
        //    [Description("Access")]
        //    Access = 49,
        //    [EnumMember]
        //    [Description("Login")]
        //    Login = 50,
        //    [EnumMember]
        //    [Description("ChangePassword")]
        //    ChangePassword = 51,
        //    [EnumMember]
        //    [Description("TemplatePermission")]
        //    TemplatePermission = 62,
        //}


        /// <summary>
        /// این تگ برای درخواست به سیستم فایل منیجیر کاربرد دارد
        /// </summary>

    }
}
