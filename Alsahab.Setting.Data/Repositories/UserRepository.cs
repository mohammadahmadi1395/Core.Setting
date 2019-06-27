// to be added
// using System;
// using System.Linq;
// using System.Threading;
// using System.Threading.Tasks;
// using Alsahab.Common;
// using Alsahab.Common.Exceptions;
// using Alsahab.Common.Utilities;
// using Alsahab.Setting.Data.Contracts;
// using Alsahab.Setting.Entities;
// using Microsoft.EntityFrameworkCore;

// namespace Alsahab.Setting.Data.Repositories
// {
//     public class UserRepository : Repository<User>, IUserRepository, IScopedDependency
//     {
//         public UserRepository(ApplicationDbContext dbContext)
//             : base(dbContext)
//         {
//         }

//         public Task UpdateSecurityStampAsync(User user, CancellationToken cancellationToken)
//         {
//             user.SecurityStamp = Guid.NewGuid().ToString();
//             return UpdateAsync(user, cancellationToken);
//         }

//         // می‌توان مشخص کرد که در زمان دلخواه، مثلا هر زمان که مشخصات اصلی کاربر تغییر کرد، مهر امنیتی وی به روز شود تا با توکن فعلی نتواند درخواست بفرستد
//         // public override void Update(User entity, bool saveNow = true)        
//         // {
//         //     entity.SecurityStamp = Guid.NewGuid();
//         //     base.Update(entity, saveNow);
//         // }
//         public Task<User> GetByUserAndPassAsync(string username, string password, CancellationToken cancellationToken)
//         {
//             var passwordHash = SecurityHelper.GetSha256Hash(password);
//             return Table.Where(p => p.UserName == username && p.PasswordHash == passwordHash).SingleOrDefaultAsync(cancellationToken);
//         }

//         public async Task AddAsync(User user, string password, CancellationToken cancellationToken)
//         {
//             var exists = await TableNoTracking.AnyAsync(p => p.UserName == user.UserName);
//             if (exists)
//                 // throw new Exception("نام کاربری تکراری است");
//                 throw new BadRequestException("نام کاربری تکراری است");

//             var passwordHash = SecurityHelper.GetSha256Hash(password);
//             user.PasswordHash = passwordHash;
//             await base.AddAsync(user, cancellationToken);
//         }

//         public Task UpdateLastLoginDateAsync(User user, CancellationToken cancellationToken)
//         {
//             user.LastLoginDate = DateTimeOffset.Now;
//             return UpdateAsync(user, cancellationToken);
//         }
//     }
// }