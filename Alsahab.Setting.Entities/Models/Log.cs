using System;
using System.Collections.Generic;
using Alsahab.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alsahab.Setting.Entities.Models
{
    public class Log : BaseEntity<Log, LogDTO, long>
    {
        public long UserID { get; set; }
        public int EntityID { get; set; }
        public int ActionTypeID { get; set; }
        public long? RecordID { get; set; }
        public string Message { get; set; }
        public long? GroupID { get; set; }
        public long? RegistrantPersonID { get; set; }
        public string RegistrantPersonFullName { get; set; }
        public string GroupName { get; set; }
    }

    public class LogConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> entity)
        {
            entity.Property(e => e.ID).HasColumnName("ID");

            entity.Property(e => e.ActionTypeID).HasColumnName("ActionTypeID");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");

            entity.Property(e => e.EntityID).HasColumnName("EntityID");

            entity.Property(e => e.GroupID).HasColumnName("GroupID");

            entity.Property(e => e.GroupName).HasMaxLength(50);

            entity.Property(e => e.RecordID).HasColumnName("RecordID");

            entity.Property(e => e.RegistrantPersonFullName).HasMaxLength(100);

            entity.Property(e => e.RegistrantPersonID).HasColumnName("RegistrantPersonID");

            entity.Property(e => e.UserID).HasColumnName("UserID");
        }
    }
}
