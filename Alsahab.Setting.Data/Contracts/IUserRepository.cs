// // to be added
// using System.Threading;
// using System.Threading.Tasks;
// using Alsahab.Setting.Entities;

// namespace Alsahab.Setting.Data.Contracts
// {
//     public interface IUserDL : IBaseDL<User>
//     {
//         Task<User> GetByUserAndPassAsync(string username, string password, CancellationToken cancellationToken);
//         Task AddAsync(User user, string password, CancellationToken cancellationToken);
//         Task UpdateSecurityStampAsync(User user, CancellationToken cancellationToken);
//         Task UpdateLastLoginDateAsync(User user, CancellationToken requestAborted);
//     }
// }