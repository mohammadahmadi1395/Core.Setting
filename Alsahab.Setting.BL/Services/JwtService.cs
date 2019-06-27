// using System;
// using System.Collections.Generic;
// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Text;
// using System.Threading.Tasks;
// using Alsahab.Common;
// using Alsahab.Setting.Entities;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.Extensions.Options;
// using Microsoft.IdentityModel.Tokens;

// namespace Alsahab.Setting.Services.Services
// {
//     public class JwtService : IJwtService, IScopedDependency
//     {
//         private readonly SiteSettings _sitesettings;
//         private readonly SignInManager<User> _signInManager;
//         public JwtService(IOptionsSnapshot<SiteSettings> settings, SignInManager<User> signInManager)
//         {
//             _sitesettings = settings.Value;
//             _signInManager = signInManager;
//         }
//         public async Task<AccessToken> GenerateAsync(User user)
//         {
//             var secretKey = Encoding.UTF8.GetBytes(_sitesettings.JwtSettings.SecretKey); //logner than 16 character
//             var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

//             //for ewt
//             var encrythonKey = Encoding.UTF8.GetBytes(_sitesettings.JwtSettings.EncryptKey);
//             var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encrythonKey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

//             var claims = await _getClaimsAsync(user);
//             var descriptor = new SecurityTokenDescriptor
//             {
//                 Issuer = _sitesettings.JwtSettings.Issuer,
//                 Audience = _sitesettings.JwtSettings.Audience,
//                 IssuedAt = DateTime.Now,
//                 NotBefore = DateTime.Now.AddMinutes(_sitesettings.JwtSettings.NotBeforeMinutes),
//                 Expires = DateTime.Now.AddMinutes(_sitesettings.JwtSettings.ExpiratenMinutes),
//                 SigningCredentials = signingCredentials,
//                 EncryptingCredentials = encryptingCredentials, // for ewt
//                 Subject = new ClaimsIdentity(claims)
//             };

//             //برای جلوگیری از تبدیل دو نوع کلیم تایپ به همدیگر سه خط کد زیر را می‌نویسیم
//             // JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
//             // JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
//             // JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

//             var tokenHandler = new JwtSecurityTokenHandler();
//             // var securityToken = tokenHandler.CreateToken(descriptor);
//             // هنگام جایگزین کردن
//             //jwt to oauth
//             // روش جایگزین اول:
//             // var securityToken = tokenHandler.CreateToken(descriptor) as JwtSecurityToken;
//             // روش جایگزین دوم:
//             var securityToken = tokenHandler.CreateJwtSecurityToken(descriptor);

//             // var jwt = tokenHandler.WriteToken(securityToken);

//             return new AccessToken(securityToken);
//         }

//         private async Task<IEnumerable<Claim>> _getClaimsAsync(User user)
//         {
//             #region By Identity
//             //لیست تمام کلیم‌های یوزر را می‌دهد
//             var result = await _signInManager.ClaimsFactory.CreateAsync(user);
//             // add custom claims
//             var list = new List<Claim>(result.Claims);
//             list.Add(new Claim(ClaimTypes.MobilePhone, "09127581368"));
//             return list;
//             #endregion

//             #region Normal way
//             // var securityStampClaimType = new ClaimsIdentityOptions().SecurityStampClaimType;
//             // //برای دسترسی به انواع 
//             // // claim
//             // // میتوان از 
//             // // ClaimTypes. or JwtRegisteredClaimNames.
//             // // استفاده کرد
//             // var list = new List<Claim>
//             // {
//             //     new Claim(ClaimTypes.Name, user.UserName),
//             //     new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
//             //     new Claim(ClaimTypes.MobilePhone, "09127581368"),
//             //     new Claim(securityStampClaimType, user.SecurityStamp.ToString())
//             // };

//             // var roles = new Role[] { new Role { Name = "Admin" } };
//             // foreach (var role in roles)
//             //     list.Add(new Claim(ClaimTypes.Role, role.Name));
//             // return list;
//             #endregion            
//         }
//     }
// }