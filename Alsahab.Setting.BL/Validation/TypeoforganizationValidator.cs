// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using Alsahab.Setting.DTO;
// using Alsahab.Common.Validation;

// namespace Alsahab.Setting.BL.Validation
// {
//     internal class TypeoforganizationValidator : Alsahab.Setting.DTO.Validation.TypeoforganizationValidator
//     {
//         TypeoforganizationBL TypeoforganizationBL = new TypeoforganizationBL();
//         public TypeoforganizationValidator() : base()
//         {

//             RuleFor(x => x.Title).Must(NotExist).When(x => !string.IsNullOrWhiteSpace(x.Title)).WithMessage(ValidatorOptions.LanguageManager.GetString("NotExist"));
//         }

//         private bool NotExist(string title)
//         {
//             var result = TypeoforganizationBL.TypeoforganizationGet(new TypeoforganizationDTO { Title = title })?.ToList();
//             var Count = result.Where(s => s.Title == title)?.Count();
//             if (Count > 0)
//             {
//                 return false;
//             }
//             return true;
//         }

//     }
// }
