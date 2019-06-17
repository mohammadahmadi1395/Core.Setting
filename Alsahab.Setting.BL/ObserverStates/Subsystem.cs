// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using Alsahab.Setting.DTO;

// namespace Alsahab.Setting.BL.Observers.ObserverStates
// {
//     public class SubsystemAdd : ObserverStateBase
//     {
//         public override Enums.LogActionType Type
//         {
//             get
//             {
//                 return Enums.LogActionType.SubsystemAdd;
//             }
//         }
//         public SubsystemDTO Subsystem { get; set; }
//     }
//     public class SubsystemEdit : ObserverStateBase
//     {
//         public override Enums.LogActionType Type
//         {
//             get
//             {
//                 return Enums.LogActionType.SubsystemEdit;
//             }
//         }
//         public SubsystemDTO Subsystem { get; set; }
//     }
//     public class SubsystemDelete : ObserverStateBase
//     {
//         public override Enums.LogActionType Type
//         {
//             get
//             {
//                 return Enums.LogActionType.SubsystemDelete;
//             }
//         }
//         public SubsystemDTO Subsystem { get; set; }
//     }
// }
