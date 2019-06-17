// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using Alsahab.Setting.DTO;

// namespace Alsahab.Setting.BL.Observers.ObserverStates
// {
//     public class TypeoforganizationAdd : ObserverStateBase
//     {
//         public override Enums.LogActionType Type
//         {
//             get
//             {
//                 return Enums.LogActionType.TypeoforganizationAdd;
//             }
//         }
//         public TypeoforganizationDTO Typeoforganization { get; set; }
//     }
//     public class TypeoforganizationEdit : ObserverStateBase
//     {
//         public override Enums.LogActionType Type
//         {
//             get
//             {
//                 return Enums.LogActionType.TypeoforganizationEdit;
//             }
//         }
//         public TypeoforganizationDTO Typeoforganization { get; set; }
//     }
//     public class TypeoforganizationDelete : ObserverStateBase
//     {
//         public override Enums.LogActionType Type
//         {
//             get
//             {
//                 return Enums.LogActionType.TypeoforganizationDelete;
//             }
//         }
//         public TypeoforganizationDTO Typeoforganization { get; set; }
//     }
// }
