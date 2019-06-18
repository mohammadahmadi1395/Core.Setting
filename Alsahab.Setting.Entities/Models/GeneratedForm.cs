// using System;
// using System.Collections.Generic;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;

// namespace Alsahab.Setting.Entities.Models
// {
//     public class GeneratedForm : BaseEntity
//     {
//         // public long Id { get; set; }
//         public string PublicCode { get; set; }
//         public string PrivateCode { get; set; }
//         public long SubsystemId { get; set; }
//         public long UniqeCode { get; set; }
//         public DateTime CreateDate { get; set; }
//         public bool IsDeleted { get; set; }
//     }

//     public class GeneratedFormConfiguration : IEntityTypeConfiguration<GeneratedForm>
//     {
//         public void Configure(EntityTypeBuilder<GeneratedForm> entity)
//         {
//             entity.Property(e => e.Id).HasColumnName("ID");

//             entity.Property(e => e.CreateDate).HasColumnType("datetime");

//             entity.Property(e => e.PrivateCode)
//                 .IsRequired()
//                 .HasMaxLength(50);

//             entity.Property(e => e.PublicCode)
//                 .IsRequired()
//                 .HasMaxLength(50);

//             entity.Property(e => e.SubsystemId).HasColumnName("SubsystemID");
//         }
//     }
// }
