// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using Alsahab.Setting.DTO;

// namespace Alsahab.Setting.BL.Observers.ObserverStates
// {
//     public class PrefixAdd : ObserverStateBase
//     {
//         public override Enums.LogActionType Type
//         {
//             get
//             {
//                 return Enums.LogActionType.PrefixAdd;
//             }
//         }
//         public PrefixDTO Prefix { get; set; }
//     }
//     public class PrefixEdit : ObserverStateBase
//     {
//         public override Enums.LogActionType Type
//         {
//             get
//             {
//                 return Enums.LogActionType.PrefixEdit;
//             }
//         }
//         public PrefixDTO Prefix { get; set; }
//     }
//     public class PrefixDelete : ObserverStateBase
//     {
//         public override Enums.LogActionType Type
//         {
//             get
//             {
//                 return Enums.LogActionType.PrefixDelete;
//             }
//         }
//         public PrefixDTO Prefix { get; set; }
//     }
// }
