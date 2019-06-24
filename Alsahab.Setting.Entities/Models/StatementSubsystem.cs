// using System;
// using System.Collections.Generic;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;

// namespace Alsahab.Setting.Entities.Models
// {
//     public class StatementSubsystem : BaseEntity
//     {
//         // public long Id { get; set; }
//         public long StatementId { get; set; }
//         public long SubsystemId { get; set; }

//         public Statement Statement { get; set; }
//         public Subsystem Subsystem { get; set; }
//         public bool IsDeleted {get;set;}
//         public DateTime CreateDate {get;set;}
//     }

//     public class StatementSubsystemConfiguration : IEntityTypeConfiguration<StatementSubsystem>
//     {
//         public void Configure(EntityTypeBuilder<StatementSubsystem> entity)
//         {
//             entity.Property(e => e.Id).HasColumnName("ID");

//             entity.Property(e => e.StatementId).HasColumnName("StatementID");

//             entity.Property(e => e.SubsystemId).HasColumnName("SubsystemID");

//             entity.HasOne(d => d.Statement)
//                 .WithMany(p => p.StatementSubsystem)
//                 .HasForeignKey(d => d.StatementId)
//                 .OnDelete(DeleteBehavior.ClientSetNull)
//                 .HasConstraintName("FK_StatementSubsystem_Statement");

//             entity.HasOne(d => d.Subsystem)
//                 .WithMany(p => p.StatementSubsystem)
//                 .HasForeignKey(d => d.SubsystemId)
//                 .OnDelete(DeleteBehavior.ClientSetNull)
//                 .HasConstraintName("FK_StatementSubsystem_Subsystem");
//         }
//     }
// }
