// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using Alsahab.Setting.DTO;
// using Alsahab.Common;
// using Newtonsoft.Json;

// namespace Alsahab.Setting.BL.Observers.ActionDTO
// {
//     public class GeneratedFormActionDTO : ActionBaseDTO
//     {
//         public override DTO.Enums.SettingEntity Entity
//         {
//             get
//             {
//                 return DTO.Enums.SettingEntity.GeneratedForm;
//             }
//         }
//         public GeneratedFormDTO GeneratedForm { get; set; }
//         public override string DisplayMessage
//         {
//             get
//             {
//                 return JsonConvert.SerializeObject('\u202B' + "کد عمومی : " + GeneratedForm?.PublicCode + "، کد اختصاصی : " + GeneratedForm?.PrivateCode);
//             }
//         }
//     }
// }
