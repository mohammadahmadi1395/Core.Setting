// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using FluentValidation;

// namespace Alsahab.Setting.DTO {
//     public class FormTypeDTO : BaseDTO//<FormTypeDTO, FormType>
//     {
//         public String Title { get; set; }
//         public long? SubSystemID { get; set; }
//         public Enums.RequestType? Enum { get; set; }
//         public String PublicCode { get; set; }
//         public String Coment { get; set; }

//         //
//         public String SubSystemTitle { get; set; }
//         public String SubSystemShortName { get; set; }
//     }

//     public class FormTypeValidator : AbstractValidator<FormTypeDTO> {
//         public FormTypeValidator () {
//             RuleFor (x => x.Title).NotEmpty ();
//             RuleFor (x => x.SubSystemID).NotEmpty ();
//             RuleFor (x => x.PublicCode).NotEmpty ();
//             RuleFor (x => x.PublicCode).MinimumLength (4);
//             RuleFor (x => x.IsDeleted).NotEqual (true);
//         }
//     }

// }