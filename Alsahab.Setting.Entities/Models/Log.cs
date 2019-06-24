// using System;
// using System.Collections.Generic;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;

// namespace Alsahab.Setting.Entities.Models
// {
//     public class Log : BaseEntity
//     {
//         // public long Id { get; set; }
//         public long UserId { get; set; }
//         //public DateTime CreateDate { get; set; }
//         //public bool IsDeleted {get;set;}
//         public int EntityId { get; set; }
//         public int ActionTypeId { get; set; }
//         public long? RecordId { get; set; }
//         public string Message { get; set; }
//         public long? GroupId { get; set; }
//         public long? RegistrantPersonId { get; set; }
//         public string RegistrantPersonFullName { get; set; }
//         public string GroupName { get; set; }
//     }

//     public class LogConfiguration : IEntityTypeConfiguration<Log>
//     {
//         public void Configure(EntityTypeBuilder<Log> entity)
//         {
//             entity.Property(e => e.Id).HasColumnName("ID");

//             entity.Property(e => e.ActionTypeId).HasColumnName("ActionTypeID");

//             entity.Property(e => e.CreateDate).HasColumnType("datetime");

//             entity.Property(e => e.EntityId).HasColumnName("EntityID");

//             entity.Property(e => e.GroupId).HasColumnName("GroupID");

//             entity.Property(e => e.GroupName).HasMaxLength(50);

//             entity.Property(e => e.RecordId).HasColumnName("RecordID");

//             entity.Property(e => e.RegistrantPersonFullName).HasMaxLength(100);

//             entity.Property(e => e.RegistrantPersonId).HasColumnName("RegistrantPersonID");

//             entity.Property(e => e.UserId).HasColumnName("UserID");
//         }
//     }
// }
