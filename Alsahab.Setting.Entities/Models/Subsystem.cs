// using System;
// using System.Collections.Generic;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;

// namespace Alsahab.Setting.Entities.Models
// {
//     public class Subsystem : BaseEntity
//     {
//         // public long Id { get; set; }
//         public string Name { get; set; }
//         public string ShortName { get; set; }
//         public DateTime? CreateDate { get; set; }
//         public bool? IsDeleted { get; set; }
//         public string Description { get; set; }
//         public bool? IsSystem { get; set; }
//         public bool? IsActive { get; set; }
//         public int? RunOrder { get; set; }
//         public bool IsPart { get; set; }

//         public ICollection<FormType> FormType { get; set; }
//         public ICollection<StatementSubsystem> StatementSubsystem { get; set; }
//         public ICollection<Subpart> Subpart { get; set; }
//     }

//     public class SubsystemConfiguration : IEntityTypeConfiguration<Subsystem>
//     {
//         public void Configure(EntityTypeBuilder<Subsystem> entity)
//         {
//                 entity.Property(e => e.Id).HasColumnName("ID");

//                 entity.Property(e => e.CreateDate).HasColumnType("datetime");

//                 entity.Property(e => e.Description)
//                     .IsRequired()
//                     .HasMaxLength(50);

//                 entity.Property(e => e.Name)
//                     .IsRequired()
//                     .HasMaxLength(50);

//                 entity.Property(e => e.ShortName).HasMaxLength(50);
//         }

//     }
// }
