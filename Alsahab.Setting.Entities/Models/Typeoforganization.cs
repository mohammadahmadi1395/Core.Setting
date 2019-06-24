// using System;
// using System.Collections.Generic;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;

// namespace Alsahab.Setting.Entities.Models
// {
//     public class Typeoforganization : BaseEntity
//     {
//         // public long Id { get; set; }
//         public string Title { get; set; }
//         //public bool IsDeleted { get; set; }
//         //public DateTime CreateDate { get; set; }
//     }

//     public class TypeoforganizationConfiguration : IEntityTypeConfiguration<Typeoforganization>
//     {
//         public void Configure(EntityTypeBuilder<Typeoforganization> entity)
//         {
//             entity.Property(e => e.Id).HasColumnName("ID");

//             entity.Property(e => e.CreateDate).HasColumnType("datetime");

//             entity.Property(e => e.Title)
//                 .IsRequired()
//                 .HasMaxLength(50);
//         }

//     }
// }
