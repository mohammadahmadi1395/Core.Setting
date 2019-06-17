using System;
using System.IdentityModel.Tokens.Jwt;

namespace Alsahab.Setting.BL.Services
{
    // این کلاس مربوط به خروجی استاندارد 
    // OAuth
    // است. به عبارتی استاندارد 
    // جی دبلیو تی را به استاندارد 
    // OAuth
    // تبدیل می‌کند
    public class AccessToken
    {
        public string access_token {get;set;}
        public string refresh_token {get;set;}
        public string token_type {get;set;}
        public int expires_in {get;set;}

        public AccessToken(JwtSecurityToken securityToken)
        {
            // متن توکن
            access_token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            // نوع توکن
            token_type = "Bearer";
            // مدت زمان اعتبار توکن
            expires_in = (int)(securityToken.ValidTo - DateTime.UtcNow).TotalSeconds;
        }
    }
}