// using System;
// using System.Collections.Generic;
// using System.Threading;
// using System.Threading.Tasks;
// using Alsahab.Common.Exceptions;
// using Alsahab.Setting.Data.Contracts;
// using Alsahab.Setting.Entities;
// using Alsahab.Setting.MyAPI.Models;
// using Alsahab.Setting.Services.Services;
// using Alsahab.Setting.WebFramework.Api;
// using Alsahab.Setting.WebFramework.Filter;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Logging;
// using Swashbuckle.AspNetCore.Filters;

// namespace Alsahab.Setting.MyAPI.Controllers.v1
// {

//     // [Route("api/[controller]")]
//     // [ApiResultFilter]
//     // //نکته: اگر بخواهیم یک نقش را در این جا قرار دهیم، همه توابع فقط برای این بخش قابل دسترسی هستند.
//     // // اگر بخواهیم یک تابع، به طور خاص برای نقشهای دیگر هم قابل دسترسی باشد، بالای همان اکشن
//     // // AllowAnonymous
//     // // را وارد می‌کنیم
//     // //فقط نقش نویسنده اجازه دسترسی به این اکشن را دارد
//     // [Authorize(Roles = "Admin")]
//     // [ApiController]
//     [ApiVersion("1")]
//     public class UserController : BaseController//ControllerBase
//     {
//         private readonly IUserRepository userRepository;
//         private readonly ILogger<UserController> logger;
//         private readonly IJwtService jwtService;
//         private readonly UserManager<User> userManager;
//         private readonly RoleManager<Role> roleManager;
//         private readonly SignInManager<User> signInManager;

//         public UserController(IUserRepository userRepository,
//             ILogger<UserController> logger,
//             IJwtService jwtService,
//             UserManager<User> userManager,
//             RoleManager<Role> roleManager,
//             SignInManager<User> signInManager)
//         {
//             this.userRepository = userRepository;
//             this.logger = logger;
//             this.jwtService = jwtService;
//             this.userManager = userManager;
//             this.roleManager = roleManager;
//             this.signInManager = signInManager;
//         }

//         // action = Token
//         /// <summary>
//         /// This method generates token
//         /// </summary>
//         /// <param name="tokenRequest">Information about token request</param>
//         /// <param name="cancellationToken">nothing</param>
//         /// <returns></returns>
//         // تصمیم داریم که توکن هم توسط متد گت و هم توسط متد ست قابل فراخوانی باشد
//         [Route("[action]"), HttpPost, HttpGet]
//         [AllowAnonymous]
//         // می‌توانیم مشخص کنیم که نحوه دریافت پارامترها از کویری استرینگ باشد یا از فرم
//         // ما می‌خواهیم که از هر کدام بود، قابل قبول باشد
//         // به همین منظور در لیست پارامترهای ورودی، می‌گوییم که از کویری استرینگ بگیرد، اما اگر خالی بود، در داخل متد از فرم می‌گیریم یا بالعکس
//         public virtual async Task<ActionResult> Token([FromForm]TokenRequest tokenRequest, CancellationToken cancellationToken)
//         {
//             if (!tokenRequest.grant_type.Equals("password", StringComparison.OrdinalIgnoreCase))
//                 throw new Exception("OAuth flow is not password.");
//             //By Identity
//             var user = await userManager.FindByNameAsync(tokenRequest.username);
//             if (user == null)
//                 throw new BadRequestException("نام کاربری اشتباه است");
//             bool passwordIsValid = await userManager.CheckPasswordAsync(user, tokenRequest.password);
//             if (!passwordIsValid)
//                 throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است");

//             await userManager.UpdateSecurityStampAsync(user);

//             // //Normal way
//             // // var user = await userRepository.GetByUserAndPassAsync(username, password, cancellationToken);
//             // if (user == null)
//             //     throw new BadRequestException("نام کاربری یا رمز عبور اشتباه است");

//             var token = await jwtService.GenerateAsync(user);
//             return new JsonResult(token);
//         }

//         [HttpGet]
//         public virtual async Task<List<User>> Get(CancellationToken cancellationToken)
//         {
//             // زمانی که از آیدنتیتی استفاده می‌کنیم نیاز به چند سطر پایین را نداریم

//             // //برای آن که مشخصات کاربر لاگین کننده را دریافت کنیم، به صورت زیر عمل می‌کنیم
//             // //userName
//             // var userName = HttpContext.User.Identity.GetUserName();
//             // userName = HttpContext.User.Identity.Name;
//             // //userId
//             // var userId = HttpContext.User.Identity.GetUserId();
//             // var userIdInt = HttpContext.User.Identity.GetUserId<int>();
//             // //other fields
//             // var phone = HttpContext.User.Identity.FindFirstValue(ClaimTypes.MobilePhone);
//             // var role = HttpContext.User.Identity.FindFirstValue(ClaimTypes.Role);

//             var users = await userRepository.TableNoTracking.ToListAsync(cancellationToken);
//             return users;
//         }

//         [HttpGet("{id:int}")]
//         // باعث میشود که این اکشن نیاز به احراز هویت نداشته باشد
//         [AllowAnonymous]
//         public virtual async Task<ApiResult<User>> Get(int id, CancellationToken cancellationToken)
//         {
//             //Identity
//             var user2 = await userManager.FindByIdAsync(id.ToString());
//             // var role = await roleManager.FindByNameAsync("Admin");

//             // //برای دریافت اطلاعات یوزر لاگین کننده 
//             // var u = await userManager.GetUserAsync(HttpContext.User);
//             //Normal
//             var user = await userRepository.GetByIdAsync(cancellationToken, id);
//             if (user == null)
//                 return NotFound();
//             return user;
//         }

//         [SwaggerRequestExample(typeof(UserDto), typeof(ExampleUserDto))]
//         [HttpPost]
//         [AllowAnonymous]
//         public virtual async Task<ApiResult<User>> Create(UserDto userDto, CancellationToken cancellationToken)
//         {
//             // var exists = await userRepository.TableNoTracking.AnyAsync(p=>p.UserName == userDto.UserName);
//             // if (exists)
//             //     return BadRequest("نام کاربری نمی‌تواند تکراری باشد");

//             // //Normal
//             var user = new User
//             {
//                 Age = userDto.Age,
//                 FullName = userDto.FullName,
//                 Gender = userDto.Gender,
//                 IsActive = userDto.IsActive ?? true,
//                 UserName = userDto.UserName,
//                 Email = userDto.Email,
//             };
//             // await userRepository.AddAsync(user, userDto.Password, cancellationToken);
//             // return Ok(user); 

//             //By Identity
//             var result = await userManager.CreateAsync(user, userDto.Password);

//             // اضافی برای تست
//             // var roleResult = await roleManager.CreateAsync(new Role { Name = "Admin", Description = "admin" });
//             var result3 = await userManager.AddToRoleAsync(user, "Admin");

//             return Ok(user);
//         }

//         [HttpPut]
//         public virtual async Task<IActionResult> Update(int id, User user, CancellationToken cancellationToken)
//         {
//             var updateUser = await userRepository.GetByIdAsync(cancellationToken, id);

//             updateUser.UserName = user.UserName;
//             updateUser.PasswordHash = user.PasswordHash;
//             updateUser.FullName = user.FullName;
//             updateUser.Age = user.Age;
//             updateUser.Gender = user.Gender;
//             updateUser.IsActive = user.IsActive;
//             updateUser.LastLoginDate = user.LastLoginDate;

//             // در مواقع حساس بهتر است
//             // security stamp
//             // مربوط به یوزر را تغییر دهیم تا توکن قبلی کاربر غیرفعال شود
//             await userManager.UpdateSecurityStampAsync(user);

//             await userRepository.UpdateAsync(updateUser, cancellationToken);

//             return Ok();
//         }

//         [HttpDelete]
//         public virtual async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
//         {
//             var user = await userRepository.GetByIdAsync(cancellationToken, id);
//             await userRepository.DeleteAsync(user, cancellationToken);
//             return Ok();
//         }
//     }

//     public class ExampleUserDto : IExamplesProvider
//     {
//         public object GetExamples()
//         {
//             return new UserDto
//             {
//                 Email = "test@test.com",
//                 UserName = "mohammad_ahmadi",
//                 FullName = "mohammad ahmadi",
//                 Gender = GenderType.Male,
//                 Age = 33
//             };
//         }
//     }
// }