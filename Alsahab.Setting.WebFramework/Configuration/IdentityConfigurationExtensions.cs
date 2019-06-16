// To be added
// using System;
// using Alsahab.Setting.Common;
// using Alsahab.Setting.Data;
// using Alsahab.Setting.Entities;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.Extensions.DependencyInjection;

// public static class IdenetiyConfigurationExtensions
// {
//     public static void AddCustomIdentity(this IServiceCollection services, IdentitySettings identitySettings)
//     {
//         services.AddIdentity<User, Role>(options =>
//          {
//             //Password Settings
//              options.Password.RequireDigit = identitySettings.PasswordRequiredDigit;
//              options.Password.RequiredLength = identitySettings.PasswordRequiredLength;
//              options.Password.RequireNonAlphanumeric = identitySettings.PasswordRequiredNonAlphanumeric;
//              options.Password.RequireLowercase = identitySettings.PasswrodRequiredLowercase;
//              options.Password.RequireUppercase = identitySettings.PasswrodRequiredUppercase;
//             //UserName Settings
//             options.User.RequireUniqueEmail = identitySettings.RequireUniqueEmail;
//             //SignIn Settings
//             // options.SignIn.RequireConfirmedEmail = false;
//             //  options.SignIn.RequireConfirmedPhoneNumber = false;

//             // نکته: ویژگی‌های زیر برای جی دابلیو تی کار نمی‌کنند و تنها برای زمانی به درد می‌خورد که بخواهیم از کوکی استفاده کنیم
//             // //Lockout Settings
//             // options.Lockout.MaxFailedAccessAttempts = 5;
//             // options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
//             // options.Lockout.AllowedForNewUsers = false; 
//         })
//         .AddEntityFrameworkStores<ApplicationDbContext>()
//         .AddDefaultTokenProviders();
//     }

// }