// using System.Collections.Generic;
// using System.Threading;
// using System.Threading.Tasks;
// using Alsahab.Setting.DL.Contracts;
// using Alsahab.Setting.Entities;
// using Alsahab.Setting.MyAPI.Models;
// using Alsahab.Setting.Services.Services;
// using Alsahab.Setting.WebFramework.Api;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Logging;

// namespace Alsahab.Setting.MyAPI.Controllers.v2
// {

//     [ApiVersion("2")]
//     public class UserController : v1.UserController
//     {
//         public UserController(IUserRepository userRepository,
//             ILogger<UserController> logger,
//             IJwtService jwtService,
//             UserManager<User> userManager,
//             RoleManager<Role> roleManager,
//             SignInManager<User> signInManager)
//             : base(userRepository, logger, jwtService, userManager, roleManager, signInManager)
//         {
//         }

//         public override Task<ActionResult> Token([FromForm]TokenRequest tokenRequest, CancellationToken cancellationToken)
//         {
//             return base.Token(tokenRequest, cancellationToken);
//         }

//         public override Task<List<User>> Get(CancellationToken cancellationToken)
//         {
//             return base.Get(cancellationToken);
//         }

//         public override Task<ApiResult<User>> Get(int id, CancellationToken cancellationToken)
//         {
//             return base.Get(id, cancellationToken);
//         }

//         public override Task<ApiResult<User>> Create(UserDto userDto, CancellationToken cancellationToken)
//         {
//             return base.Create(userDto, cancellationToken);
//         }

//         public override Task<IActionResult> Update(int id, User user, CancellationToken cancellationToken)
//         {
//             return base.Update(id, user, cancellationToken);
//         }

//         public override Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
//         {
//             return base.Delete(id, cancellationToken);
//         }
//     }
// }