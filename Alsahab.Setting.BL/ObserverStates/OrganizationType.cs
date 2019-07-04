// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using Alsahab.Setting.DTO;

// namespace Alsahab.Setting.BL.Observers.ObserverStates
// {
//     public class OrganizationTypeAdd : ObserverStateBase
//     {
//         public override Enums.LogActionType Type
//         {
//             get
//             {
//                 return Enums.LogActionType.OrganizationTypeAdd;
//             }
//         }
//         public OrganizationTypeDTO OrganizationType { get; set; }
//     }
//     public class OrganizationTypeEdit : ObserverStateBase
//     {
//         public override Enums.LogActionType Type
//         {
//             get
//             {
//                 return Enums.LogActionType.OrganizationTypeEdit;
//             }
//         }
//         public OrganizationTypeDTO OrganizationType { get; set; }
//     }
//     public class OrganizationTypeDelete : ObserverStateBase
//     {
//         public override Enums.LogActionType Type
//         {
//             get
//             {
//                 return Enums.LogActionType.OrganizationTypeDelete;
//             }
//         }
//         public OrganizationTypeDTO OrganizationType { get; set; }
//     }
// }
