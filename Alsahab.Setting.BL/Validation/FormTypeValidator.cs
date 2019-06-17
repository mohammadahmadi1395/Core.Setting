
// using Alsahab.Common.Validation;
// using Alsahab.Setting.DTO;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

// namespace Alsahab.Setting.BL.Validation
// {
//     internal class FormTypeValidator : Alsahab.Setting.DTO.Validation.FormTypeValidator
//     {

//         public FormTypeValidator(): base()
//         {
//             RuleFor(x => x.Title).Must(NotExist).When(x => !string.IsNullOrWhiteSpace(x.Title)).WithMessage(ValidatorOptions.LanguageManager.GetString("NotExist"));
//         }

//         private bool NotExist(string title)
//         {
//             FormTypeBL FormTypeBL = new FormTypeBL();
//             var result = FormTypeBL.FormTypeGet(new FormTypeDTO { Title = title });
//             var Count = result.Where(s => s.Title == title)?.Count();
//             if (Count > 0)
//             {
//                 return false;
//             }
//             return true;
//         }

//     }
// }
