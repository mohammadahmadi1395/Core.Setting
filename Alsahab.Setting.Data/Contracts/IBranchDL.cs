// using System.Threading;
// using System.Threading.Tasks;
// using Alsahab.Setting.DTO;
// using Alsahab.Setting.Entities;
// using Alsahab.Setting.Entities.Models;

// namespace Alsahab.Setting.Data.Contracts
// {
//     public interface IBranchDL : IBaseDL<Branch, BranchDTO, BranchFilterDTO>
//     {
//         Task<List<BranchDTO>> GetAsync(string username, string password, CancellationToken cancellationToken);
//         IList<BranchDTO> Get(BranchDTO dto);
//     }
// }