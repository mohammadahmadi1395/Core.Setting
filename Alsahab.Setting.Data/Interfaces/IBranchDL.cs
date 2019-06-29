// using System.Collections.Generic;
// using System.Threading;
// using System.Threading.Tasks;
// using Alsahab.Common;
// using Alsahab.Setting.DTO;
// using Alsahab.Setting.Entities.Models;

// namespace Alsahab.Setting.Data.Interfaces
// {
//     public interface IBranchDL : IBaseDL<Branch, BranchDTO, BranchFilterDTO>
//     {
//         Task<IList<BranchDTO>> GetAsync(BranchFilterDTO dto, CancellationToken cancellationToken, PagingInfoDTO pagnig);
//         IList<BranchDTO> Get(BranchFilterDTO dto, PagingInfoDTO paging);
//     }
// }