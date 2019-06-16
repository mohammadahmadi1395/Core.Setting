using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Setting.Entities;
using Microsoft.IdentityModel.Tokens;

namespace Alsahab.Setting.Services.Services
{
    public interface IJwtService
    {
        // با افزودن
        // OAuth
        // خروجی این متد از رشته به اکسس توکن تغییر کرد
        // Task<AccessToken> GenerateAsync(User user);
    }
}