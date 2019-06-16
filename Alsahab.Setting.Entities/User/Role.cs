// using System;
// using System.Collections.Generic;
// using System.ComponentModel.DataAnnotations;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;

// namespace Alsahab.Setting.Entities
// {
//     public class Role : IdentityRole<int>, IEntity // BaseEntity
//     {
//         // [Required]
//         // [StringLength(50)]
//         // public string Name { get; set; }
//         [Required]
//         [StringLength(100)]
//         public string Description { get; set; }
//     }

//     public class RoleConfiguration : IEntityTypeConfiguration<Role>
//     {
//         public void Configure(EntityTypeBuilder<Role> builder)
//         {
//             builder.Property(p=>p.Name).IsRequired().HasMaxLength(50);
//         }
//     }
// }