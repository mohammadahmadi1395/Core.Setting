// using System;
// using System.Collections.Generic;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;

// namespace Alsahab.Setting.Entities.Models
// {
//     public class FormType : BaseEntity
//     {
//         public string Title { get; set; }
//         public int? EnumId { get; set; }
//         public long SubSystemId { get; set; }
//         public string PublicCode { get; set; }
//         public string Coment { get; set; }
//         //public bool IsDeleted { get; set; }
//         //public DateTime CreateDate { get; set; }

//         public Subsystem SubSystem { get; set; }
//         public ICollection<RuleTag> RuleTag { get; set; }
//     }
//     public class FormTypeConfiguration : IEntityTypeConfiguration<FormType>
//     {
//         public void Configure(EntityTypeBuilder<FormType> entity)
//         {
//             entity.Property(e => e.Id).HasColumnName("ID");

//             entity.Property(e => e.CreateDate).HasColumnType("datetime");

//             entity.Property(e => e.EnumId).HasColumnName("EnumID");

//             entity.Property(e => e.PublicCode)
//                 .IsRequired()
//                 .HasMaxLength(50);

//             entity.Property(e => e.SubSystemId).HasColumnName("SubSystemID");

//             entity.Property(e => e.Title)
//                 .IsRequired()
//                 .HasMaxLength(50);

//             entity.HasOne(d => d.SubSystem)
//                 .WithMany(p => p.FormType)
//                 .HasForeignKey(d => d.SubSystemId)
//                 .OnDelete(DeleteBehavior.ClientSetNull)
//                 .HasConstraintName("FK_FormType_Subsystem");
//         }
//     }

// }
