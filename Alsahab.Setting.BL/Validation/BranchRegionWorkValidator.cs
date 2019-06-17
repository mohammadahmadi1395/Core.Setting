// using Alsahab.Setting.DTO;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using FluentValidation;
// using Gostar.Setting.BL;

// namespace Alsahab.Setting.BL.Validation
// {
//     internal class BranchRegionWorkValidator : Alsahab.Setting.DTO.BranchRegionWorkValidator
//     {
//         public BranchRegionWorkValidator():base()
//         {
//             RuleFor(x => x.BranchID).Must((DTO, BranchID) => BranchRegionCheck(DTO)).When(x => x.ZoneID > 0 && x.BranchID > 0);
//         }
//         private bool BranchRegionCheck(BranchRegionWorkDTO data)
//         {

//             var Branch = new BranchBL().BranchGet(new BranchDTO { IsCentral = true })?.FirstOrDefault();
//             if (!(Branch.ID == data.BranchID))
//             {
//                 var AllRegions = new BranchRegionWorkBL().BranchRegionWorkGet(new BranchRegionWorkDTO());
//                 foreach (var Reg in AllRegions)
//                 {
//                     if (Reg.BranchID == Branch.ID)
//                         continue;
//                     if (Reg.BranchID != data.BranchID && Reg.ZoneAndParents.Contains(data.ZoneID ?? 0))
//                         return false;
//                     if (Reg.BranchID != data.BranchID && Reg.ZoneAndChilds.Contains(data.ZoneID ?? 0))
//                         return false;
//                 }
//             }
//             return true;
//         }
//     }
// }
