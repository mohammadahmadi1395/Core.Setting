using System;
using System.ComponentModel.DataAnnotations;

namespace Alsahab.Common.Api
{    
    public enum ApiResultStatusCode
    {
        [Display(Name = "عملیات با موفقیت انجام شد")]
        Success = 0,
        [Display(Name = "خطایی در سرور رخ داد")]
        ServerError = 1,
        [Display(Name = "اشکال در درخواست")]
        BadRequest = 2,
        [Display(Name = "نتیجه‌ای یافت نشد")]
        NotFound = 3,
        [Display(Name = "لیست خالی است")]
        ListEmpty = 4,
        [Display(Name = "خطایی در پردازش رخ داد")]
        LogicError = 5,
        [Display(Name = "خطای احراز هویت")]
        Unauthorized = 6
    }
}