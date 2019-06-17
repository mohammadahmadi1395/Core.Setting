
// using Alsahab.Common.Validation;
// using Alsahab.Setting.DTO;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

// namespace Alsahab.Setting.BL.Validation
// {
//     internal class SubpartValidator : Alsahab.Setting.DTO.Validation.SubpartValidator
//     {
//         public SubpartValidator() : base()
//         {
//             RuleFor(x => x.Name).Must((DTO, Name) => NotExist(DTO, DTO.Name)).When(x => !string.IsNullOrWhiteSpace(x.Name)).WithMessage(ValidatorOptions.LanguageManager.GetString("NotExist"));


//         }
//         private bool NotExist(SubpartDTO dto, string title)
//         {
//             SubpartBL SubpartBL = new SubpartBL();
//             var result = SubpartBL.SubpartGet(new SubpartDTO { Name = title, SubsystemID = dto.SubsystemID });
//             var Count = result.Where(s => s.Name == title)?.Count();
//             if (Count > 0)
//             {
//                 return false;
//             }
//             return true;
//         }

//     }
// }
