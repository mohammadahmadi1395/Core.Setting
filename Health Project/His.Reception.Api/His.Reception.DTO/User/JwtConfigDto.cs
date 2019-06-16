using System;
using System.Collections.Generic;
using System.Text;

namespace His.Reception.DTO.User
{
    public class JwtConfigDto
    {
        public string JwtKey { get; set; }
        public string JwtIssuer { get; set; }
        public string JwtExpireDays { get; set; }
    }
}
