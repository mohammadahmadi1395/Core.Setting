using System;

namespace Alsahab.Setting.Common
{
    public class SiteSettings
    {
        public JwtSettings JwtSettings { get; set; }
        // public int MyProperty { get; set; }
        public IdentitySettings IdentitySettings { get; set; }

    }

    public class IdentitySettings
    {
        public bool PasswordRequiredDigit { get; set; }
        public int PasswordRequiredLength { get; set; }
        public bool PasswordRequiredNonAlphanumeric { get; set; }
        public bool PasswrodRequiredUppercase { get; set; }
        public bool PasswrodRequiredLowercase { get; set; }
        public bool RequireUniqueEmail { get; set; }
    }
    public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int NotBeforeMinutes { get; set; }
        public int ExpiratenMinutes { get; set; }
        public string EncryptKey { get; set; }
    }
}