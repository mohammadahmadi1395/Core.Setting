// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;

// namespace Gostar.Setting.BL
// {
//    public class ZoneBranchBL : BaseBL
//     {


//         public List<long> ZoneBranchGet(long branchid)
//         {
//             BranchRegionWorkBL BranchRegionWorkBL = new BranchRegionWorkBL();
//             BranchRegionWorkBL.User = User;

//             var Branch = BranchRegionWorkBL.BranchRegionWorkGet(new DTO.BranchRegionWorkDTO { BranchID = branchid });
//             var m = ((Branch.Select(s => s.ZoneAndChilds)).Union(Branch.Select(s => s.ZoneAndParents)))?.SelectMany(s=> s).Union(Branch.Select(s => s.ZoneID ?? 0)).Distinct()?.ToList();
//             if (m == null)
//             {
//                 ResponseStatus =Common.ResponseStatus.BusinessError;
//                     return null;           
//             }
//             ResponseStatus = Common.ResponseStatus.Successful;

//             return m;
//         }
//     }
// }
