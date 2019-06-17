using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Alsahab.Setting.Common.Validation
{
    /// <summary>
    /// All Error Message 
    /// </summary>
   public class ErrorLanguageManager : FluentValidation.Resources.LanguageManager
    {
        /// <summary>
        /// 
        /// </summary>
        public ErrorLanguageManager()
        {
            #region Base
            ////English
            //AddTranslation("en", "EmailValidator", "'{PropertyName}' is not a valid email address.");
            //AddTranslation("en", "GreaterThanOrEqualValidator", "'{PropertyName}' must be greater than or equal to '{ComparisonValue}'.");
            //AddTranslation("en", "GreaterThanValidator", "'{PropertyName}' must be greater than '{ComparisonValue}'.");
            //AddTranslation("en", "LengthValidator", "'{PropertyName}' must be between {MinLength} and {MaxLength} characters. You entered {TotalLength} characters.");
            //AddTranslation("en", "MinimumLengthValidator", "The length of '{PropertyName}' must be at least {MinLength} characters. You entered {TotalLength} characters.");
            //AddTranslation("en", "MaximumLengthValidator", "The length of '{PropertyName}' must be {MaxLength} characters or fewer. You entered {TotalLength} characters.");
            //AddTranslation("en", "LessThanOrEqualValidator", "'{PropertyName}' must be less than or equal to '{ComparisonValue}'.");
            //AddTranslation("en", "LessThanValidator", "'{PropertyName}' must be less than '{ComparisonValue}'.");
            //AddTranslation("en", "NotEmptyValidator", "'{PropertyName}' must not be empty.");
            //AddTranslation("en", "NotEqualValidator", "'{PropertyName}' must not be equal to '{ComparisonValue}'.");
            //AddTranslation("en", "NotNullValidator", "'{PropertyName}' must not be empty.");
            //AddTranslation("en", "PredicateValidator", "The specified condition was not met for '{PropertyName}'.");
            //AddTranslation("en", "AsyncPredicateValidator", "The specified condition was not met for '{PropertyName}'.");
            //AddTranslation("en", "RegularExpressionValidator", "'{PropertyName}' is not in the correct format.");
            //AddTranslation("en", "EqualValidator", "'{PropertyName}' must be equal to '{ComparisonValue}'.");
            //AddTranslation("en", "ExactLengthValidator", "'{PropertyName}' must be {MaxLength} characters in length. You entered {TotalLength} characters.");
            //AddTranslation("en", "InclusiveBetweenValidator", "'{PropertyName}' must be between {From} and {To}. You entered {Value}.");
            //AddTranslation("en", "ExclusiveBetweenValidator", "'{PropertyName}' must be between {From} and {To} (exclusive). You entered {Value}.");
            //AddTranslation("en", "CreditCardValidator", "'{PropertyName}' is not a valid credit card number.");
            //AddTranslation("en", "ScalePrecisionValidator", "'{PropertyName}' must not be more than {ExpectedPrecision} digits in total, with allowance for {ExpectedScale} decimals. {Digits} digits and {ActualScale} decimals were found.");
            //AddTranslation("en", "EmptyValidator", "'{PropertyName}' must be empty.");
            //AddTranslation("en", "NullValidator", "'{PropertyName}' must be empty.");
            //AddTranslation("en", "EnumValidator", "'{PropertyName}' has a range of values which does not include '{PropertyValue}'.");
            ////Arabic
            //AddTranslation("ar", "EmailValidator", "'{PropertyName}' ليس بريد الكتروني صحيح.");
            //AddTranslation("ar", "GreaterThanOrEqualValidator", "'{PropertyName}' يجب أن يكون أكبر من أو يساوي '{ComparisonValue}'.");
            //AddTranslation("ar", "GreaterThanValidator", "'{PropertyName}' يجب أن يكون أكبر من '{ComparisonValue}'.");
            //AddTranslation("ar", "LargthValidator", "'{PropertyName}' عدد الحروف يجب أن يكون بين {MinLargth} و {MaxLargth}. عدد ما تم ادخاله {TotalLargth}.");
            //AddTranslation("ar", "MinimumLargthValidator", "الحد الأدنى لعدد الحروف في '{PropertyName}' هو {MinLargth}. عدد ما تم ادخاله {TotalLargth}.");
            //AddTranslation("ar", "MaximumLargthValidator", "الحد الأقصى لعدد الحروف في '{PropertyName}' هو {MaxLargth}. عدد ما تم ادخاله {TotalLargth}.");
            //AddTranslation("ar", "LessThanOrEqualValidator", "'{PropertyName}' يجب أن يكون أقل من أو يساوي '{ComparisonValue}'.");
            //AddTranslation("ar", "LessThanValidator", "'{PropertyName}' يجب أن يكون أقل من '{ComparisonValue}'.");
            //AddTranslation("ar", "NotEmptyValidator", "'{PropertyName}' لا يجب أن يكون فارغاً.");
            //AddTranslation("ar", "NotEqualValidator", "'{PropertyName}' يجب ألا يساوي '{ComparisonValue}'.");
            //AddTranslation("ar", "NotNullValidator", "'{PropertyName}' لا يجب أن يكون فارغاً.");
            //AddTranslation("ar", "PredicateValidator", "الشرط المحدد لا يتفق مع '{PropertyName}'.");
            //AddTranslation("ar", "AsyncPredicateValidator", "الشرط المحدد لا يتفق مع '{PropertyName}'.");
            //AddTranslation("ar", "RegularExpressionValidator", "'{PropertyName}' ليس بالتنسيق الصحيح.");
            //AddTranslation("ar", "EqualValidator", "'{PropertyName}' يجب أن يساوي '{ComparisonValue}'.");
            //AddTranslation("ar", "ExactLargthValidator", "الحد الأقصى لعدد الحروف في '{PropertyName}' هو {MaxLargth}. عدد ما تم ادخاله {TotalLargth}.");
            //AddTranslation("ar", "InclusiveBetwearValidator", "'{PropertyName}' يجب أن يكون بين {From} و {To}. ما تم ادخاله {Value}.");
            //AddTranslation("ar", "ExclusiveBetwearValidator", "'{PropertyName}' يجب أن يكون بين {From} و {To} (حصرياً). ما تم ادخاله {Value}.");
            //AddTranslation("ar", "CreditCardValidator", "'{PropertyName}' ليس رقم بطاقة ائتمان صحيح.");
            //AddTranslation("ar", "ScalePrecisionValidator", "'{PropertyName}' لا يجب أن يكون أكبر من {ExpectedPrecision} رقما صحيحاً في المجمل, ومسموح بـ {ExpectedScale} أرقام عشرية. ما تم ادخاله {Digits} أرقام صحيحة و {ActualScale} أرقام عشرية.");
            //AddTranslation("ar", "EmptyValidator", "'{PropertyName}' يجب أن يكون فارغاً.");
            //AddTranslation("ar", "NullValidator", "'{PropertyName}' يجب أن يكون فارغاً.");
            //AddTranslation("ar", "EnumValidator", "'{PropertyName}' يحتوي على مجموعة من القيم التي لا تتضمن '{PropertyValue}'.");
            ////Persian
            //AddTranslation("fa", "EmailValidator", "'{PropertyName}' وارد شده قالب صحیح یک ایمیل را ندارد.");
            //AddTranslation("fa", "GreaterThanOrEqualValidator", "'{PropertyName}' باید بیشتر یا مساوی '{CompfaisonValue}' باشد.");
            //AddTranslation("fa", "GreaterThanValidator", "'{PropertyName}' باید بیشتر از '{CompfaisonValue}' باشد.");
            //AddTranslation("fa", "LengthValidator", "'{PropertyName}' باید حداقل {MinLength} و حداکثر {MaxLength} کاراکتر داشته باشد. اما مقدار وارد شده {TotalLength} کاراکتر دارد.");
            //AddTranslation("fa", "MinimumLengthValidator", "'{PropertyName}' باید بزرگتر یا برابر با {MinLength} کاراکتر باشد. شما شخصیت {TotalLength} را وارد کردید");
            //AddTranslation("fa", "MaximumLengthValidator", "'{PropertyName}' باید کمتر یا مساوی {MaxLength} باشد. {TotalLength} را وارد کردید");
            //AddTranslation("fa", "LessThanOrEqualValidator", "'{PropertyName}' باید کمتر یا مساوی '{CompfaisonValue}' باشد.");
            //AddTranslation("fa", "LessThanValidator", "'{PropertyName}' باید کمتر از '{CompfaisonValue}' باشد.");
            //AddTranslation("fa", "NotEmptyValidator", " ضروری است.");
            //AddTranslation("fa", "NotEqualValidator", "'{PropertyName}' نباید برابر با '{CompfaisonValue}' باشد.");
            //AddTranslation("fa", "NotNullValidator", "وارد کردن '{PropertyName}' ضروری است.");
            //AddTranslation("fa", "PredicateValidator", "شرط تعیین شده برای '{PropertyName}' برقرار نیست.");
            //AddTranslation("fa", "AsyncPredicateValidator", "شرط تعیین شده برای '{PropertyName}' برقرار نیست.");
            //AddTranslation("fa", "RegulfaExpressionValidator", "'{PropertyName}' دارای قالب صحیح نیست.");
            //AddTranslation("fa", "EqualValidator", "مقادیر وارد شده برای '{PropertyName}' و '{CompfaisonValue}' یکسان نیستند.");
            //AddTranslation("fa", "ExactLengthValidator", "'{PropertyName}' باید دقیقا {MaxLength} کاراکتر باشد اما مقدار وارد شده {TotalLength} کاراکتر دارد.");
            //AddTranslation("fa", "InclusiveBetweenValidator", "'{PropertyName}' باید بین {From} و {To} باشد. اما مقدار وارد شده ({Value}) در این محدوده نیست.");
            //AddTranslation("fa", "ExclusiveBetweenValidator", "'{PropertyName}' باید بیشتر از {From} و کمتر از {To} باشد. اما مقدار وارد شده ({Value}) در این محدوده نیست.");
            //AddTranslation("fa", "CreditCfadValidator", "'{PropertyName}' وارد شده معتبر نیست.");
            //AddTranslation("fa", "ScalePrecisionValidator", "'{PropertyName}' نباید بیش از {ExpectedPrecision} رقم، شامل {ExpectedScale} رقم اعشار داشته باشد. مقدار وارد شده {Digits} رقم و {ActualScale} رقم اعشار دارد.");
            //AddTranslation("fa", "EmptyValidator", "'{PropertyName}' باید خالی باشد.");
            //AddTranslation("fa", "NullValidator", "'{PropertyName}' باید خالی باشد.");
            //AddTranslation("fa", "EnumValidator", "مقدار '{PropertyValue}' در لیست مقادیر قابل قبول برای '{PropertyName}' نمی باشد.");

            #endregion

            #region Custom
            // زمانی که قیلد وارد شده باید منخصر به قرد باشد و تکراری نداشته باشد
            AddTranslation("en", "NotExist", "'{PropertyName}' is exist");
            AddTranslation("fa", "NotExist", "وجود دارد'{PropertyName}'");
            AddTranslation("ar", "NotExist", "هذا البند موجود'{PropertyName}'");
            //زمانی که فیلد وارد شده دیفالت است و در جدول هنوز رکوردی داریم که مقدار دیفالت آن ست شده است
            AddTranslation("en", "Default", " The default value is already there.");
            AddTranslation("fa", "Default", "مقدار پیش فرض در حال حاضر وجود دارد.");
            AddTranslation("ar", "Default", "هذا البند موجودالقيمة الافتراضية موجودة بالفعل.");
            // زمتنی که درشبع مرکزی وارد شده است و در عین حال شعبه مرکزی داریم
            AddTranslation("en", "NotCentral", " This Branch Can't Be Central,Because Central Branch Is Exist! ");
            AddTranslation("fa", "NotCentral", "این شعبه نمی تواند مرکزی باشد چرا که شعبه مرکزی وجود دارد!");
            AddTranslation("ar", "NotCentral", "لا يمكن أن يكون هذا الفرع مركزيًا ، لأن الفرع المركزي موجود!");

            #endregion


        }
    }
}
