// using System;
// using System.Text;
// using System.Collections.Generic;
// using System.ComponentModel.DataAnnotations;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;

// namespace Alsahab.Setting.Entities
// {
//     public class User : IdentityUser<int>, IEntity // BaseEntity
//     {
//         public User()
//         {
//             IsActive = true;
//             //چون خود 
//             // identity
//             // securitystamp
//             // را هندل می‌کند نیازی به این بخش نداریم
//             // SecurityStamp = Guid.NewGuid();
//         }

//         // [Required]
//         // [StringLength(100)]
//         // public string UserName { get; set; }
//         // [Required]
//         // [StringLength(500)]
//         // public string PasswordHash { get; set; }
//         [Required]
//         [StringLength(100)]
//         public string FullName { get; set; }
//         public int Age { get; set; }
//         public GenderType Gender { get; set; }
//         public bool IsActive { get; set; }
//         public DateTimeOffset? LastLoginDate { get; set; }
//         // public Guid SecurityStamp { get; set; }

//         public ICollection<Post> Posts { get; set; }
//     }

//     public enum GenderType
//     {
//         [Display(Name = "مرد")]
//         Male = 1,
//         [Display(Name = "زن")]
//         Female = 2
//     }

//     //برای اضافه کردن قیود دیتابیسی
//     public class UserConfiguration : IEntityTypeConfiguration<User>
//     {
//         public void Configure(EntityTypeBuilder<User> builder)
//         {
//             builder.Property(p => p.UserName).IsRequired().HasMaxLength(100);
//             builder.Property(p => p.PasswordHash).IsRequired().HasMaxLength(500);
//         }
//     }
// }
