using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Alsahab.Common
{
    public class PagingInfoDTO
    {
        public bool IsPaging { get; set; }
        public int Size { get; set; }
        public int Index { get; set; }
    }
    public class StatementDTO : BaseDTO
    {
        public string TagName { get; set; }
        public long? FilterSubsystemID { get; set; }
        public List<long?> SubsystemIDList { get; set; }
        public List<string> SubsystemNameList { get; set; }
        //public StatementTypes TypeID { get; set; }
        //public string TypeTitle { get { return ((StatementTypes)TypeID).GetDescription(); } }
        public string PersianText { get; set; }
        public string EnglishText { get; set; }
        public string ArabicText { get; set; }
    }

    public class UserInfoDTO
    {
        public String UserFullName { get; set; }
        public long? UserID { get; set; }
        public string UserName { get; set; }
        public Boolean? IsActive { get; set; }
        public Boolean? IsSystem { get; set; }
        public Boolean? IsDeleted { get; set; }
        public RoleType? UserRoleType { get; set; }
        //public List<long?> TeamMembers { get; set; }
        public long? UserPersonID { get; set; }
        public long? GroupID { get; set; }
        public string GroupName { get; set; }
        public long? GroupBranchID { get; set; }
        public string GroupBranchTitle { get; set; }
        public bool? GroupBranchIsCentral { get; set; }
        public List<long?> BranchAccessList { get; set; }
    }

    public class EnumDTO
    {
        public string Title { get; set; }
        public int ID { get; set; }
    }

    public abstract class BaseDTO
    {
        public long? ID { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? CreateDateFrom { get; set; }
        public DateTime? CreateDateTo { get; set; }
    }



    public class LogDTO : BaseDTO
    {
        public string Time
        {
            get
            {
                return CreateDate == null || CreateDate == DateTime.MinValue ? string.Empty : ((DateTime)this.CreateDate).ToString("HH:mm:ss");
            }
        }

        public string Date
        {
            get
            {
                return this.CreateDate == null || this.CreateDate == DateTime.MinValue ? string.Empty : ((DateTime)this.CreateDate).ToString("yyyy/MM/dd");
            }
        }
        public long UserID { set; get; }
        //public string UserName { get; set; }
        public string GroupName { get; set; }
        public long? GroupID { get; set; }
        public RoleType UserRoleType { get; set; }
        public long RegistrantPersonID { get; set; }
        public string RegistrantPersonFullName { get; set; }
        //public string GroupMembersFullName { get; set; }
        //public List<long?> GroupMembersID { get; set; }
        public int ActionTypeID { set; get; }
        public string ActionTypeTitle { get; set; }
        public int EntityID { set; get; }
        public string EntityTitle { get; set; }
        public string MessageStr { get; set; }
        public long RecordID { get; set; }
        public long? BranchID { get; set; }
        public string BranchTitle { get; set; }
    }

    public class LogFilterDTO : LogDTO
    {
        public DateTime? FromDate { set; get; }
        public DateTime? ToDate { set; get; }
        //public TimeSpan FromTime { set; get; }
        //public TimeSpan ToTime { set; get; }
        public List<long> UserIDS { set; get; }
        public List<int> ActionTypeIDs { set; get; }
        public List<int> EntityIDs { set; get; }
        public string Message { set; get; }
        public List<long> BranchIDs { set; get; }
        public List<long> GroupIDs { set; get; }
        public string FullName { set; get; }
        public List<int> UserRoleTypes { get; set; }
    }

    public class AddressDTO
    {
        public long? SectorID { get; set; }
        public String Address { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        //Non Required
        public String SectorTitle { get; set; }

        public long? CountryID { get; set; }
        public String CountryTitle { get; set; }
        public long? CityID { get; set; }
        public String CityTitle { get; set; }
        public long? AreaID { get; set; }
        public String AreaTitle { get; set; }
        public long? RegionID { get; set; }
        public String RegionTitle { get; set; }

        //Non Required

        public String ShowAddress
        {
            get
            {
                return String.Format("{0} - {1} - {2} - {3} - {4} - {5}", CountryTitle, CityTitle, AreaTitle, RegionTitle, SectorTitle, Address);
            }
        }
    }


    public class CurrencyToWordDTO
    {

        /// <summary>
        /// Standard Code
        /// Syrian Pound: SYP
        /// UAE Dirham: AED
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Is the currency name feminine ( Mua'anath مؤنث)
        /// ليرة سورية : مؤنث = true
        /// درهم : مذكر = false
        /// </summary>
        public Boolean IsCurrencyNameFeminine { get; set; }

        /// <summary>
        /// English Currency Name for single use
        /// Syrian Pound
        /// UAE Dirham
        /// </summary>
        public string EnglishCurrencyName { get; set; }

        /// <summary>
        /// English Plural Currency Name for Numbers over 1
        /// Syrian Pounds
        /// UAE Dirhams
        /// </summary>
        public string EnglishPluralCurrencyName { get; set; }

        /// <summary>
        /// Arabic Currency Name for 1 unit only
        /// ليرة سورية
        /// درهم إماراتي
        /// </summary>
        public string SingleCurrencyName { get; set; }

        /// <summary>
        /// Arabic Currency Name for 2 units only
        /// ليرتان سوريتان
        /// درهمان إماراتيان
        /// </summary>
        public string DualCurrencyName { get; set; }

        /// <summary>
        /// Arabic Currency Name for 3 to 10 units
        /// خمس ليرات سورية
        /// خمسة دراهم إماراتية
        /// </summary>
        public string PluralCurrencyName { get; set; }

        /// <summary>
        /// Arabic Currency Name for 11 to 99 units
        /// خمس و سبعون ليرةً سوريةً
        /// خمسة و سبعون درهماً إماراتياً
        /// </summary>
        public string Arabic1199CurrencyName { get; set; }

        /// <summary>
        /// Decimal Part Precision
        /// for Syrian Pounds: 2 ( 1 SP = 100 parts)
        /// for Tunisian Dinars: 3 ( 1 TND = 1000 parts)
        /// </summary>
        public Byte PartPrecision { get; set; }

        /// <summary>
        /// Is the currency part name feminine ( Mua'anath مؤنث)
        /// هللة : مؤنث = true
        /// قرش : مذكر = false
        /// </summary>
        public Boolean IsCurrencyPartNameFeminine { get; set; }

        /// <summary>
        /// English Currency Part Name for single use
        /// Piaster
        /// Fils
        /// </summary>
        public string EnglishCurrencyPartName { get; set; }

        /// <summary>
        /// English Currency Part Name for Plural
        /// Piasters
        /// Fils
        /// </summary>
        public string EnglishPluralCurrencyPartName { get; set; }

        /// <summary>
        /// Arabic Currency Part Name for 1 unit only
        /// قرش
        /// هللة
        /// </summary>
        public string SingleCurrencyPartName { get; set; }

        /// <summary>
        /// Arabic Currency Part Name for 2 unit only
        /// قرشان
        /// هللتان
        /// </summary>
        public string DualCurrencyPartName { get; set; }

        /// <summary>
        /// Arabic Currency Part Name for 3 to 10 units
        /// قروش
        /// هللات
        /// </summary>
        public string PluralCurrencyPartName { get; set; }

        /// <summary>
        /// Arabic Currency Part Name for 11 to 99 units
        /// قرشاً
        /// هللةً
        /// </summary>
        public string Arabic1199CurrencyPartName { get; set; }


    }
    [DataContract]
    public enum StatementTypes
    {
        [EnumMember]
        [Description("Nothing")]
        Nothing = 0,
        [EnumMember]
        [Description("Control")]
        Control = 1,
        [EnumMember]
        [Description("Message")]
        Message = 2,
    }
    [DataContract]
    public enum RuleType
    {
        [EnumMember]
        [Description("عمومی")]
        Public = 0,
        [EnumMember]
        [Description("اختصاصی")]
        Private = 1,

    }

    [DataContract]
    public enum ActionType
    {
        [EnumMember]
        [Description("Insert New")]
        Insert = 0,
        [EnumMember]
        [Description("Edit")]
        Update = 1,
        [EnumMember]
        [Description("Delete")]
        Delete = 2,
        [EnumMember]
        [Description("-----")]
        Nothing = 3,
        [EnumMember]
        [Description("Select")]
        Select = 4,
        [EnumMember]
        [Description("Login")]
        Login = 5,
        [EnumMember]
        [Description("Select Permission")]
        SelectPermission = 6,
        [EnumMember]
        [Description("فایل آپلود")]
        FileUpload = 7,
        [EnumMember]
        [Description("فایل دانلود")]
        FileDownload = 8,
        [EnumMember]
        [Description("Calculate and Process")]
        Calculate = 9,
        [EnumMember]
        [Description("Disable and Cancel")]
        Cancel = 10,

        [EnumMember]
        [Description("ارسال پیامک")]
        SendSMS = 11,

        [EnumMember]
        [Description("ارسال USSD")]
        SendUSSD = 12,

        [EnumMember]
        [Description("تماس")]
        Call = 13,

        [EnumMember]
        [Description("تحويل پیامک")]
        SMSDelivery = 14,

    }

    [DataContract]
    public enum ResponseStatus
    {
        [EnumMember]
        [Description("Unknown error")]
        [Display(Name = "خطای ناشناخته")]
        UnknownError = 0,

        [EnumMember]
        [Description("The operation was successful")]
        [Display(Name = "عملیات با موفقیت انجام شد")]
        Successful = 1,

        [EnumMember]
        [Description("Invalid input")]
        [Display(Name = "اشکال در درخواست")]
        BadRequest = 2,

        [EnumMember]
        [Description("Database error")]
        [Display(Name = "خطای پایگاه داده")]
        DatabaseError = 3,

        [EnumMember]
        [Description("Internal Service Error")]
        [Display(Name = "خطایی در سرور رخ داد")]
        ServerError = 4,

        [EnumMember]
        [Description("Not Found")]
        [Display(Name = "نتیجه‌ای یافت نشد")]
        NotFound = 5,

        [EnumMember]
        [Description("Login Error")]
        [Display(Name = "خطای لاگین")]
        LoginError = 6,

        [EnumMember]
        [Description("Authorization Error")]
        [Display(Name = "خطای احراز هویت")]
        Unauthorized = 7
    }

    [DataContract]
    public enum ButtonType
    {
        [EnumMember]
        [Description("Search")]
        SearchButton = 0,
        [EnumMember]
        [Description("Cancel")]
        CancelButton = 1,
        [EnumMember]
        [Description("Submit")]
        OkButton = 2,
        [EnumMember]
        [Description("Add")]
        AddButton = 3,
        [EnumMember]
        [Description("Save")]
        SaveButton = 4,
        [EnumMember]
        [Description("Reset")]
        ResetButton = 5,
        [EnumMember]
        [Description("Print")]
        PrintButton = 6,
        [EnumMember]
        [Description("Refresh")]
        RefreshButton = 7,

        [EnumMember]
        [Description("Back")]
        BackButton = 8,

        [EnumMember]
        [Description("Next")]
        NextButton = 9,

        [EnumMember]
        [Description("Delete")]
        DeleteButton = 10,

        [EnumMember]
        [Description("Attach")]
        AttachButton = 11,
    }

    [DataContract]
    public enum RoleType
    {
        [EnumMember]
        [Description("کاربر شخصی")]
        SingleUser = 0,
        [EnumMember]
        [Description("کاربر تیم کشف")]
        DiscoveryTeamUser = 1,
        [EnumMember]
        [Description("مدیر کشف")]
        DiscoveryManagerUser = 2,
        [EnumMember]
        [Description("کاربر تیم تدقیق")]
        ValidationTeamUser = 3,
        [EnumMember]
        [Description("مدیر کل")]
        MainManagerUser = 4,
        [EnumMember]
        [Description("تشریفات")]
        InQueryUser = 5,
        [EnumMember]
        [Description("ادمین")]
        AdminUser = 6,
        [EnumMember]
        [Description("کاربر گروه کفالت")]
        BailsmentUser = 7,
        [EnumMember]
        [Description("مدیر کفالت")]
        BailsmentManager = 8,
        [EnumMember]
        [Description("مدیر اعضا")]
        MemberManager = 9,
        [EnumMember]
        [Description("مدیر خانواده ها")]
        OrphanPoorManager = 10,
        [EnumMember]
        [Description("جمع آوری کننده")]
        Collector = 11,
        /// <summary>
        /// Box Collector,
        /// اون کسی که صندوق ها را جمع آوری میکند
        /// </summary>
        [EnumMember]
        [Description("جمع آوری کننده صندوق")]
        BoxCollector = 12,

    }

    [DataContract]
    public enum PermissionType
    {
        [EnumMember]
        [Description]
        View = 0,
        [EnumMember]
        [Description("افزودن")]
        Insert = 1,
        [EnumMember]
        [Description("ویرایش")]
        Edit = 2,
        [EnumMember]
        [Description("حذف")]
        Delete = 3,
        [EnumMember]
        [Description("چاپ")]
        Print = 4,
        [EnumMember]
        [Description("آپلود فایل")]
        FileUpload = 5,
    }

    [DataContract]
    public enum Level
    {
        [EnumMember]
        [Description(" ")]
        Noneofthem = 0,
        [EnumMember]
        [Description("Bad level")]
        BadLevel = 1,
        [EnumMember]
        [Description("Low level")]
        LowLevel = 2,
        [EnumMember]
        [Description("Average level")]
        AverageLevel = 3,
        [EnumMember]
        [Description("Good level")]
        GoodLevel = 4,
        [EnumMember]
        [Description("High level")]
        HighLevel = 5,
    }

    [DataContract]
    public enum FillType
    {
        [EnumMember]
        [Description("GostarType")]
        GostarType = 0,

        [EnumMember]
        [Description("MaterialType")]
        MaterialType = 1
    }

    [DataContract]
    public enum Language
    {
        [EnumMember]
        [Description("arabic_IQ")]
        ar_IQ = 0,
        [EnumMember]
        [Description("english_US")]
        en_US = 1,
        [EnumMember]
        [Description("farsi_IR")]
        fa_IR = 2,
    }

    [DataContract]
    public enum AcceptType
    {
        [EnumMember]
        [Description("Int")]
        Int = 0,
        [EnumMember]
        [Description("Decimal")]
        Decimal = 1,
        [EnumMember]
        [Description("Currency")]
        Currency = 2,

        [EnumMember]
        [Description("String")]
        String = 3,


    }

    [DataContract]
    public enum FilterType
    {
        [EnumMember]
        [Description("Filter")]
        Filter = 0,
        [EnumMember]
        [Description("Search")]
        Search = 1,
    }
    [DataContract]
    public enum DefaultSort
    {
        [EnumMember]
        [Description("Ascending")]
        Ascending = 0,
        [EnumMember]
        [Description("Descending")]
        Descending = 1,
    }
    [DataContract]
    public enum ShowMethod
    {
        [EnumMember]
        [Description("Code")]
        CodeText = 0,
        [EnumMember]
        [Description("Text")]
        Text = 1,
    }
    [DataContract]
    public enum SortType
    {
        [EnumMember]
        [Description("WithCode")]
        Code = 0,
        [EnumMember]
        [Description("Text")]
        Text = 1,
    }

    public enum GMessageBoxIcons
    {
        [EnumMember]
        [Description("Error")]
        Error = 0,
        [EnumMember]
        [Description("Warning")]
        Warning = 1,
        [EnumMember]
        [Description("Information")]
        Information = 2,
        [EnumMember]
        [Description("Question")]
        Question = 3,
        [EnumMember]
        [Description("Success")]
        Success = 4,
    }

    public enum GMessageBoxButtons
    {
        [EnumMember]
        [Description("Ok")]
        Ok = 0,
        [EnumMember]
        [Description("YesNo")]
        YesNo = 1,
        [EnumMember]
        [Description("OkCancel")]
        OkCancel = 2,
        [EnumMember]
        [Description("YesNoCancel")]
        YesNoCancel = 3


    }

    public enum MessageType
    {
        [EnumMember]
        [Description("Error")]
        Error = 0,

        [EnumMember]
        [Description("Success")]
        Success = 1,

        [EnumMember]
        [Description("Warning")]
        Warning = 2,

        [EnumMember]
        [Description("Info")]
        Info = 3,
    }

    /// <summary>
    /// Database Error Code
    /// </summary>
    public enum SqlError
    {
        [EnumMember]
        [Description("Nothing")]
        Nothing = 0,
        [EnumMember]
        [Description("Unique")]
        Unique = 2627,
    }

    /// <summary>
    /// For Question With 2 Item
    /// </summary>
    public enum Bool
    {
        [EnumMember]
        [Description("")]
        Nothing = 0,
        [EnumMember]
        [Description("نعم")]
        Yes = 1,
        [EnumMember]
        [Description("لا")]
        No = 2,
    }

    public enum Month
    {
        [EnumMember]
        [Description("All")]
        All = 0,

        [EnumMember]
        [Description("ينايِر")]
        January = 1,

        [EnumMember]
        [Description("فبرايِر")]
        February = 2,

        [EnumMember]
        [Description("مارتش")]
        March = 3,

        [EnumMember]
        [Description("ابريل")]
        April = 4,

        [EnumMember]
        [Description("مي")]
        May = 5,

        [EnumMember]
        [Description("يونيو")]
        June = 6,

        [EnumMember]
        [Description("يوليو")]
        July = 7,

        [EnumMember]
        [Description("أغسطس")]
        August = 8,

        [EnumMember]
        [Description("سبتمبر")]
        September = 9,

        [EnumMember]
        [Description("اکتوبر")]
        October = 10,

        [EnumMember]
        [Description("نوفَمبِر")]
        November = 11,

        [EnumMember]
        [Description("ديسامبِر")]
        December = 12,
    }
}
