// using System;
// using System.Collections.Generic;
// using System.ComponentModel.DataAnnotations;
// using Alsahab.Setting.Entities;
// using Alsahab.Setting.WebFramework.Api;

// namespace Alsahab.Setting.MyAPI.Models
// {
//     public class UserDto : IValidatableObject
//     {
//         [Required]
//         [StringLength(100)]
//         public string UserName { get; set; }
//         [Required]
//         [StringLength(500)]
//         public string Password { get; set; }
//         [Required]
//         [StringLength(100)]
//         public string FullName { get; set; }
//         public int Age { get; set; }
//         public GenderType Gender { get; set; }
//         public bool? IsActive { get; set; }
//         public string Email { get; set; }

//         public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
//         {
//             if (UserName.Equals("test", StringComparison.OrdinalIgnoreCase))
//                 yield return new ValidationResult("نام کاربری نمی‌تواند Test باشد", new[] { nameof(UserName) });
//             if (Password.Equals("123456", StringComparison.OrdinalIgnoreCase))
//                 yield return new ValidationResult("کلمه عبور نمی‌تواند 123456 باشد", new[] { nameof(Password) });
//             if (Gender == GenderType.Male && Age > 30)
//                 yield return new ValidationResult("مردان بالای 30 سال مجاز نیستند", new[] { nameof(Gender), nameof(Age) });
//         }
//     }
// }