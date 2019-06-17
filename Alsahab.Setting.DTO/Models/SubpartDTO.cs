// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using FluentValidation;

// namespace Alsahab.Setting.DTO
// {
//     public class SubpartDTO : BaseDTO//<SubpartDTO, Subpart>
//     {
//         public List<long?> IDList { get; set; }
//         public long? SubsystemID { get; set; }
//         public string SubsystemName { get; set; }
//         public string Name { get; set; }
//         public string Description { get; set; }
//         public bool? IsSystem { get; set; } = false;
//         public bool? IsActive { get; set; } = true;
//     }

//     public class SubpartValidator : AbstractValidator<SubpartDTO>
//     {
//         public SubpartValidator()
//         {
//             RuleFor(x => x.SubsystemID).NotEmpty();
//             RuleFor(x => x.Name).NotEmpty();
//             RuleFor(x => x.IsDeleted).NotEqual(true);
//         }
//     }

// }