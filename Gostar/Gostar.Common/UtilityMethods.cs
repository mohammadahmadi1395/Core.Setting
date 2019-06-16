using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Text.RegularExpressions;
using Gostar.Common.Validation;
using System.Globalization;
using Gostar.Common.Validation.Results;
using System.IO;
using Newtonsoft.Json;

//using Gostar.Setting.DTO;

namespace Gostar.Common
{
    public static class UtilityMethods
    {

        #region Varaibles
        private static string[] englishOnes =
              new string[] {
            "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine",
            "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"
           };

        private static string[] englishTens =
              new string[] {
            "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"
          };

        private static string[] englishGroup =
            new string[] {
            "Hundred", "Thousand", "Million", "Billion", "Trillion", "Quadrillion", "Quintillion", "Sextillian",
            "Septillion", "Octillion", "Nonillion", "Decillion", "Undecillion", "Duodecillion", "Tredecillion",
            "Quattuordecillion", "Quindecillion", "Sexdecillion", "Septendecillion", "Octodecillion", "Novemdecillion",
            "Vigintillion", "Unvigintillion", "Duovigintillion", "10^72", "10^75", "10^78", "10^81", "10^84", "10^87",
            "Vigintinonillion", "10^93", "10^96", "Duotrigintillion", "Trestrigintillion"
        };

        #region Varaibles & Properties

        /// <summary>
        /// integer part
        /// </summary>
        private static long _intergerValue;
        private static CurrencyToWordDTO Currency = new CurrencyToWordDTO();
        /// <summary>
        /// Decimal Part
        /// </summary>
        private static int _decimalValue;
        /// <summary>
        /// Number to be converted
        /// </summary>
        private static Decimal Number { get; set; }

        /// <summary>
        /// Currency to use
        /// </summary>


        /// <summary>
        /// English text to be placed before the generated text
        /// </summary>
        private static String EnglishPrefixText { get; set; }

        /// <summary>
        /// English text to be placed after the generated text
        /// </summary>
        private static String EnglishSuffixText { get; set; }

        /// <summary>
        /// Arabic text to be placed before the generated text
        /// </summary>
        private static String ArabicPrefixText { get; set; }

        /// <summary>
        /// Arabic text to be placed after the generated text
        /// </summary>
        private static String ArabicSuffixText { get; set; }
        #endregion


        private static string[] arabicOnes =
           new string[] {
            String.Empty, "واحد", "اثنان", "ثلاثة", "أربعة", "خمسة", "ستة", "سبعة", "ثمانية", "تسعة",
            "عشرة", "أحد عشر", "اثنا عشر", "ثلاثة عشر", "أربعة عشر", "خمسة عشر", "ستة عشر", "سبعة عشر", "ثمانية عشر", "تسعة عشر"
        };

        private static string[] arabicFeminineOnes =
           new string[] {
            String.Empty, "إحدى", "اثنتان", "ثلاث", "أربع", "خمس", "ست", "سبع", "ثمان", "تسع",
            "عشر", "إحدى عشرة", "اثنتا عشرة", "ثلاث عشرة", "أربع عشرة", "خمس عشرة", "ست عشرة", "سبع عشرة", "ثماني عشرة", "تسع عشرة"
        };

        private static string[] arabicTens =
            new string[] {
            "عشرون", "ثلاثون", "أربعون", "خمسون", "ستون", "سبعون", "ثمانون", "تسعون"
        };

        private static string[] arabicHundreds =
            new string[] {
            "", "مائة", "مئتان", "ثلاثمائة", "أربعمائة", "خمسمائة", "ستمائة", "سبعمائة", "ثمانمائة","تسعمائة"
        };

        private static string[] arabicAppendedTwos =
            new string[] {
            "مئتا", "ألفا", "مليونا", "مليارا", "تريليونا", "كوادريليونا", "كوينتليونا", "سكستيليونا"
        };

        private static string[] arabicTwos =
            new string[] {
            "مئتان", "ألفان", "مليونان", "ملياران", "تريليونان", "كوادريليونان", "كوينتليونان", "سكستيليونان"
        };

        private static string[] arabicGroup =
            new string[] {
            "مائة", "ألف", "مليون", "مليار", "تريليون", "كوادريليون", "كوينتليون", "سكستيليون"
        };

        private static string[] arabicAppendedGroup =
            new string[] {
            "", "ألفاً", "مليوناً", "ملياراً", "تريليوناً", "كوادريليوناً", "كوينتليوناً", "سكستيليوناً"
        };

        private static string[] arabicPluralGroups =
            new string[] {
            "", "آلاف", "ملايين", "مليارات", "تريليونات", "كوادريليونات", "كوينتليونات", "سكستيليونات"
        };
        #endregion


        public static string GetDescription<T>(this T e) where T : IConvertible
        {
            string description = null;

            if (e is Enum)
            {
                Type type = e.GetType();
                Array values = System.Enum.GetValues(type);

                foreach (int val in values)
                    if (val == e.ToInt32(System.Globalization.CultureInfo.InvariantCulture))
                    {
                        var memInfo = type.GetMember(type.GetEnumName(val));
                        var descriptionAttributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                        if (descriptionAttributes.Length > 0)
                            description = ((DescriptionAttribute)descriptionAttributes[0]).Description;
                        break;
                    }
            }
            return description;
        }
        /// <summary>
        /// Validate EmailAddress
        /// </summary>
        /// <param name="Email"></param>
        /// <returns>
        /// True For IsValidate
        /// False For NotValidate
        /// </returns>
        public static bool EmailIsValid(string Email)
        {
            string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";
            Regex Regex = new Regex(validEmailPattern, RegexOptions.IgnoreCase);
            bool isValid = Regex.IsMatch(Email);
            return isValid;
        }

        public static string NumberToWord(decimal number, string lang, CurrencyToWordDTO currency)
        {

            Currency = currency;
            Currency.Arabic1199CurrencyName = currency.SingleCurrencyName;
            Currency.Arabic1199CurrencyPartName = currency.SingleCurrencyPartName;

            long _intergerValue;

            int _decimalValue = 0;

            Decimal Number;

            if (lang == "en-US")
            {

            }
            Number = number;



            String[] splits = Number.ToString().Split('.');

            _intergerValue = Convert.ToInt64(splits[0]);

            if (splits.Length > 1)
            {
                string decimalPart = splits[1];
                string result = String.Empty;

                if (Currency.PartPrecision != decimalPart.Length)
                {
                    int decimalPartLength = decimalPart.Length;

                    for (int i = 0; i < Currency?.PartPrecision - decimalPartLength; i++)
                    {
                        decimalPart += "0"; //Fix for 1 number after decimal ( 10.5 , 1442.2 , 375.4 ) 
                    }

                    result = String.Format("{0}.{1}", decimalPart.Substring(0, Currency.PartPrecision), decimalPart.Substring(Currency.PartPrecision, decimalPart.Length - Currency.PartPrecision));

                    result = (Math.Round(Convert.ToDecimal(result))).ToString();
                }
                else
                    result = decimalPart;

                for (int i = 0; i < Currency.PartPrecision - result.Length; i++)
                {
                    result += "0";
                }
                _decimalValue = Convert.ToInt32(result);

            }

            if (lang == "en-US")
            {

                string[] englishOnes =
              new string[] {
            "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine",
            "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"
           };

                string[] englishTens =
                    new string[] {
            "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"
                };

                string[] englishGroup =
                    new string[] {
            "Hundred", "Thousand", "Million", "Billion", "Trillion", "Quadrillion", "Quintillion", "Sextillian",
            "Septillion", "Octillion", "Nonillion", "Decillion", "Undecillion", "Duodecillion", "Tredecillion",
            "Quattuordecillion", "Quindecillion", "Sexdecillion", "Septendecillion", "Octodecillion", "Novemdecillion",
            "Vigintillion", "Unvigintillion", "Duovigintillion", "10^72", "10^75", "10^78", "10^81", "10^84", "10^87",
            "Vigintinonillion", "10^93", "10^96", "Duotrigintillion", "Trestrigintillion"
                };

                Decimal tempNumber = Number;

                if (tempNumber == 0)
                    return "Zero";

                string decimalString = ProcessGroup(_decimalValue);

                string retVal = String.Empty;

                int group = 0;

                if (tempNumber < 1)
                {
                    retVal = englishOnes[0];
                }
                else
                {
                    while (tempNumber >= 1)
                    {
                        int numberToProcess = (int)(tempNumber % 1000);

                        tempNumber = tempNumber / 1000;

                        string groupDescription = ProcessGroup(numberToProcess);

                        if (groupDescription != String.Empty)
                        {
                            if (group > 0)
                            {
                                retVal = String.Format("{0} {1}", englishGroup[group], retVal);
                            }

                            retVal = String.Format("{0} {1}", groupDescription, retVal);
                        }

                        group++;
                    }
                }

                String formattedNumber = String.Empty;
                formattedNumber += (EnglishPrefixText != String.Empty) ? String.Format("{0} ", EnglishPrefixText) : String.Empty;
                formattedNumber += (retVal != String.Empty) ? retVal : String.Empty;
                formattedNumber += (retVal != String.Empty) ? (_intergerValue == 1 ? Currency.EnglishCurrencyName : Currency.EnglishPluralCurrencyName) : String.Empty;
                formattedNumber += (decimalString != String.Empty) ? " and " : String.Empty;
                formattedNumber += (decimalString != String.Empty) ? decimalString : String.Empty;
                formattedNumber += (decimalString != String.Empty) ? " " + (_decimalValue == 1 ? Currency.EnglishCurrencyPartName : Currency.EnglishPluralCurrencyPartName) : String.Empty;
                formattedNumber += (EnglishSuffixText != String.Empty) ? String.Format(" {0}", EnglishSuffixText) : String.Empty;

                return formattedNumber;

            }

            else //اگر زبان عربی یا فارسی بود
            {

                Decimal tempNumber = Number;

                if (tempNumber == 0)
                    return "صفر";

                // Get Text for the decimal part
                string decimalString = ProcessArabicGroup(_decimalValue, -1, 0);

                string retVal = String.Empty;
                Byte group = 0;
                while (tempNumber >= 1)
                {
                    // seperate number into groups
                    int numberToProcess = (int)(tempNumber % 1000);

                    tempNumber = tempNumber / 1000;

                    // convert group into its text
                    string groupDescription = ProcessArabicGroup(numberToProcess, group, Math.Floor(tempNumber));

                    if (groupDescription != String.Empty)
                    { // here we add the new converted group to the previous concatenated text
                        if (group > 0)
                        {
                            if (retVal != String.Empty)
                                retVal = String.Format("{0} {1}", "و", retVal);

                            if (numberToProcess != 2)
                            {
                                if (numberToProcess % 100 != 1)
                                {
                                    if (numberToProcess >= 3 && numberToProcess <= 10) // for numbers between 3 and 9 we use plural name
                                        retVal = String.Format("{0} {1}", arabicPluralGroups[group], retVal);
                                    else
                                    {
                                        if (retVal != String.Empty) // use appending case
                                            retVal = String.Format("{0} {1}", arabicAppendedGroup[group], retVal);
                                        else
                                            retVal = String.Format("{0} {1}", arabicGroup[group], retVal); // use normal case
                                    }
                                }
                                else
                                {
                                    retVal = String.Format("{0} {1}", arabicGroup[group], retVal); // use normal case
                                }
                            }
                        }

                        retVal = String.Format("{0} {1}", groupDescription, retVal);
                    }

                    group++;
                }

                String formattedNumber = String.Empty;
                formattedNumber += (ArabicPrefixText != String.Empty) ? String.Format("{0} ", ArabicPrefixText) : String.Empty;
                formattedNumber += (retVal != String.Empty) ? retVal : String.Empty;
                if (_intergerValue != 0)
                { // here we add currency name depending on _intergerValue : 1 ,2 , 3--->10 , 11--->99
                    int remaining100 = (int)(_intergerValue % 100);

                    if (remaining100 == 0)
                        formattedNumber += Currency.SingleCurrencyName;
                    else
                        if (remaining100 == 1)
                        formattedNumber += Currency.SingleCurrencyName;
                    else
                            if (remaining100 == 2)
                    {
                        if (_intergerValue == 2)
                            formattedNumber += Currency.DualCurrencyName;
                        else
                            formattedNumber += Currency.SingleCurrencyName;
                    }
                    else
                                if (remaining100 >= 3 && remaining100 <= 10)
                        formattedNumber += Currency.PluralCurrencyName;
                    else
                                    if (remaining100 >= 11 && remaining100 <= 99)
                        formattedNumber += Currency.Arabic1199CurrencyName;
                }
                formattedNumber += (_decimalValue != 0) ? " و " : String.Empty;
                formattedNumber += (_decimalValue != 0) ? decimalString : String.Empty;
                if (_decimalValue != 0)
                { // here we add currency part name depending on _intergerValue : 1 ,2 , 3--->10 , 11--->99
                    formattedNumber += " ";

                    int remaining100 = (int)(_decimalValue % 100);

                    if (remaining100 == 0)
                        formattedNumber += Currency.SingleCurrencyPartName;
                    else
                        if (remaining100 == 1)
                        formattedNumber += Currency.SingleCurrencyPartName;
                    else
                            if (remaining100 == 2)
                        formattedNumber += Currency.DualCurrencyPartName;
                    else
                                if (remaining100 >= 3 && remaining100 <= 10)
                        formattedNumber += Currency.PluralCurrencyPartName;
                    else
                                    if (remaining100 >= 11 && remaining100 <= 99)
                        formattedNumber += Currency.Arabic1199CurrencyPartName;
                }
                formattedNumber += (ArabicSuffixText != String.Empty) ? String.Format(" {0}", ArabicSuffixText) : String.Empty;

                return formattedNumber;


            }
        }
        private static string ProcessArabicGroup(int groupNumber, int groupLevel, Decimal remainingNumber)
        {
            int tens = groupNumber % 100;

            int hundreds = groupNumber / 100;

            string retVal = String.Empty;

            if (hundreds > 0)
            {
                if (tens == 0 && hundreds == 2) // حالة المضاف
                    retVal = String.Format("{0}", arabicAppendedTwos[0]);
                else //  الحالة العادية
                    retVal = String.Format("{0}", arabicHundreds[hundreds]);
            }

            if (tens > 0)
            {
                if (tens < 20)
                { // if we are processing under 20 numbers
                    if (tens == 2 && hundreds == 0 && groupLevel > 0)
                    { // This is special case for number 2 when it comes alone in the group
                        if (_intergerValue == 2000 || _intergerValue == 2000000 || _intergerValue == 2000000000 || _intergerValue == 2000000000000 || _intergerValue == 2000000000000000 || _intergerValue == 2000000000000000000)
                            retVal = String.Format("{0}", arabicAppendedTwos[groupLevel]); // في حالة الاضافة
                        else
                            retVal = String.Format("{0}", arabicTwos[groupLevel]);//  في حالة الافراد
                    }
                    else
                    { // General case
                        if (retVal != String.Empty)
                            retVal += " و ";

                        if (tens == 1 && groupLevel > 0 && hundreds == 0)
                            retVal += " ";
                        else
                            if ((tens == 1 || tens == 2) && (groupLevel == 0 || groupLevel == -1) && hundreds == 0 && remainingNumber == 0)
                            retVal += String.Empty; // Special case for 1 and 2 numbers like: ليرة سورية و ليرتان سوريتان
                        else
                            retVal += GetDigitFeminineStatus(tens, groupLevel);// Get Feminine status for this digit
                    }
                }
                else
                {
                    int ones = tens % 10;
                    tens = (tens / 10) - 2; // 20's offset

                    if (ones > 0)
                    {
                        if (retVal != String.Empty)
                            retVal += " و ";

                        // Get Feminine status for this digit
                        retVal += GetDigitFeminineStatus(ones, groupLevel);
                    }

                    if (retVal != String.Empty)
                        retVal += " و ";

                    // Get Tens text
                    retVal += arabicTens[tens];
                }
            }

            return retVal;
        }
        private static string GetDigitFeminineStatus(int digit, int groupLevel)
        {
            if (groupLevel == -1)
            { // if it is in the decimal part
                if (Currency.IsCurrencyPartNameFeminine)
                    return arabicFeminineOnes[digit]; // use feminine field
                else
                    return arabicOnes[digit];
            }
            else
                if (groupLevel == 0)
            {
                if (Currency.IsCurrencyNameFeminine)
                    return arabicFeminineOnes[digit];// use feminine field
                else
                    return arabicOnes[digit];
            }
            else
                return arabicOnes[digit];
        }


        private static string ProcessGroup(int groupNumber)
        {
            int tens = groupNumber % 100;

            int hundreds = groupNumber / 100;

            string retVal = String.Empty;

            if (hundreds > 0)
            {
                retVal = String.Format("{0} {1}", englishOnes[hundreds], englishGroup[0]);
            }
            if (tens > 0)
            {
                if (tens < 20)
                {
                    retVal += ((retVal != String.Empty) ? " " : String.Empty) + englishOnes[tens];
                }
                else
                {
                    int ones = tens % 10;

                    tens = (tens / 10) - 2; // 20's offset

                    retVal += ((retVal != String.Empty) ? " " : String.Empty) + englishTens[tens];

                    if (ones > 0)
                    {
                        retVal += ((retVal != String.Empty) ? " " : String.Empty) + englishOnes[ones];
                    }
                }
            }

            return retVal;
        }
        public class LogRequest
        {

            [DataMember]
            public UserInfoDTO User { get; set; }
            [DataMember]
            public long? RequestID { get; set; }
            [DataMember]
            public Gostar.Common.ActionType ActionType { get; set; }
            [DataMember]
            public LogFilterDTO RequestDto { get; set; }
            [DataMember]
            public List<LogFilterDTO> RequestDtoList { get; set; }
            // [DataMember]
            // public Gostar.Common.Language Language { get; set; } = Language.ar_IQ;
        }

        public class LogResponse
        {
            [DataMember]
            public string ErrorMessage { get; set; }
            [DataMember]
            public Gostar.Common.ResponseStatus ResponseStatus { get; set; }
            [DataMember]
            public LogDTO ResponseDto { get; set; }
            [DataMember]
            public List<LogDTO> ResponseDtoList { get; set; }
        }

        /// <summary>
        /// Validate data With Class
        /// </summary>
        /// <typeparam name="TValidator"> Validator Class</typeparam>
        /// <typeparam name="TObject"> DTO Class </typeparam>
        /// <param name="data"> </param>
        /// <param name="culture"> Set culture For Translate Error Messages</param>
        /// <returns></returns>
        public static ValidationResult Validate<TValidator, TObject>(TObject data, CultureInfo culture = null) where TValidator : AbstractValidator<TObject>
        { 
            //Set Custom Translation
            ValidatorOptions.LanguageManager = new Gostar.Common.Validation.ErrorLanguageManager();
            //Create Instance From Validator    
            var validator = Activator.CreateInstance(typeof(TValidator));
            //Set Culture To Translate
            ValidatorOptions.LanguageManager.Culture = culture ?? CultureInfo.CurrentUICulture;
            var result = ((AbstractValidator<TObject>)validator).Validate(data);
            return result;
        }


        public static void SetConfig(String Name, String Value)
        {
          

            //configure new property
            if (!Directory.Exists(Path.GetTempPath() + "UserConfigsFromGostarApps"))
                Directory.CreateDirectory(Path.GetTempPath() + "UserConfigsFromGostarApps");
            if (!File.Exists(Path.GetTempPath() + "UserConfigsFromGostarApps//ConfigListJson.txt"))
                File.CreateText(Path.GetTempPath() + "UserConfigsFromGostarApps//ConfigListJson.txt");

            UserConfigDTO con = new UserConfigDTO
            {
                Name = Name,
                Value = Value
            };

            var Datas = GetAllConfig();

            try
            {
                Datas.Where(s => s.Name == con.Name).FirstOrDefault().Value = Value;
            }
            catch
            {
                Datas.Add(con);
            }
            SaveConfigs(Datas);
        }

        private static List<UserConfigDTO> GetAllConfig()
        {
            try
            {
                var Res = File.ReadAllText(Path.GetTempPath() + "UserConfigsFromGostarApps//ConfigListJson.txt");
                if (String.IsNullOrWhiteSpace(Res))
                    return new List<UserConfigDTO>();
                var lc = JsonConvert.DeserializeObject<List<UserConfigDTO>>(Res);
                return lc;
            }
            catch { return new List<UserConfigDTO>(); }
        }

        private static void SaveConfigs(List<UserConfigDTO> List)
        {
            try {
                var s = JsonConvert.SerializeObject(List);
                File.WriteAllText(Path.GetTempPath() + "UserConfigsFromGostarApps//ConfigListJson.txt", s);
            }
            catch { }
        }

        public static UserConfigDTO GetConfig(String Name)
        {
            var l = GetAllConfig();
            var res = l?.Where(s => s.Name == Name)?.ToList();
            if (res?.Count == 0)
                return null;
            else
                return res?.FirstOrDefault();
        }

        public static List<String> StringToColumnArray(String s)
        {
            List<String> List = new List<string>();
            var res = s.Split(',');
            foreach (var val in res)
                List.Add(val);
            return List;
        }

        public static String ColumnArrayToString(List<String> s)
        {
            return String.Join(",", s);
        }

        public static bool IsSame<Ttype>(List<Ttype> list1, List<Ttype> list2)
        {
            foreach (var item in list1)
                if (!list2.Contains(item))
                    return false;

            foreach (var item in list2)
                if (!list1.Contains(item))
                    return false;

            return true;
        }

        public static List<Ttype> GetCopy<Ttype>(List<Ttype> list)
        {
            List<Ttype> res = new List<Ttype>();
            foreach (var val in list)
                res.Add(val);
            return res;
        }

        //Only for Integrated UI
        //public static List<StatementDTO> GetLanguageXmlElements(string fileName)
        //{
        //    if (string.IsNullOrWhiteSpace(fileName))
        //        return null;

        //    XmlReader reader = XmlReader.Create(fileName, new XmlReaderSettings());
        //    var result = new List<StatementDTO>();
        //    StatementDTO tempText = new StatementDTO();
        //    int? subSysId = null;
        //    string subSysValue = string.Empty;
        //    string subSysKey = string.Empty;
        //    int? typeId = null;
        //    string typeTitle = string.Empty;
        //    while (reader.Read())
        //    {
        //        if ((reader.NodeType == XmlNodeType.Element))
        //        {
        //            if (reader.Name == "Subsystem")
        //            {
        //                if (reader.HasAttributes)
        //                {
        //                    subSysId = Convert.ToInt32(reader.GetAttribute("ID"));
        //                    subSysValue = reader.GetAttribute("Value");
        //                    subSysKey = reader.GetAttribute("Key");
        //                }
        //            }
        //            else if (reader.Name == "Type")
        //            {
        //                if (reader.HasAttributes)
        //                {
        //                    typeId = Convert.ToInt32(reader.GetAttribute("ID"));
        //                    typeTitle = ((StatementTypes)(typeId)).GetDescription();
        //                }
        //            }
        //            else if (reader.Name == "Text")
        //            {
        //                if (reader.HasAttributes)
        //                {
        //                    string arabicText = reader.GetAttribute("Arabic_Text");
        //                    string englishText = reader.GetAttribute("English_Text");
        //                    string persianText = reader.GetAttribute("Persian_Text");
        //                    string tagName = reader.GetAttribute("TagName");
        //                    result.Add(new StatementDTO
        //                    {
        //                        ArabicText = arabicText,
        //                        EnglishText = englishText,
        //                        PersianText = persianText,
        //                        SubsystemID = (long)subSysId,
        //                        TypeID = (StatementTypes)typeId,
        //                        SubsystemName = subSysKey,
        //                        //TypeTitle = typeTitle,
        //                        TagName = tagName,
        //                    });
        //                }
        //            }
        //        }
        //    }
        //    return result;
        //}

    }
}
