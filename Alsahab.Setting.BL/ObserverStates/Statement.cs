// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using Alsahab.Setting.DTO;
// using Alsahab.Common;
// using Alsahab.Setting.DTO;

// namespace Alsahab.Setting.BL.Observers.ObserverStates
// {
//     public class StatementAdd : ObserverStateBase
//     {
//         public override Enums.LogActionType Type
//         {
//             get
//             {
//                 return Enums.LogActionType.StatementAdd;
//             }
//         }
//         public DTO.StatementDTO Statement { get; set; }
//     }
//     public class StatementEdit : ObserverStateBase
//     {
//         public override Enums.LogActionType Type
//         {
//             get
//             {
//                 return Enums.LogActionType.StatementEdit;
//             }
//         }
//         public DTO.StatementDTO Statement { get; set; }
//     }
//     public class StatementDelete : ObserverStateBase
//     {
//         public override Enums.LogActionType Type
//         {
//             get
//             {
//                 return Enums.LogActionType.StatementDelete;
//             }
//         }
//         public DTO.StatementDTO Statement { get; set; }
//     }
// }
