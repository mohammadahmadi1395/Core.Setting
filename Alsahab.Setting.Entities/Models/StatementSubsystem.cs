using System;
using System.Collections.Generic;
using Alsahab.Setting.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alsahab.Setting.Entities.Models
{
    public class StatementSubsystem : BaseEntity<StatementSubsystem, StatementSubsystemDTO, long>
    {
        // public long ID { get; set; }
        public long StatementID { get; set; }
        public long SubsystemID { get; set; }

        public Statement Statement { get; set; }
        public Subsystem Subsystem { get; set; }
    }

    public class StatementSubsystemConfiguration : IEntityTypeConfiguration<StatementSubsystem>
    {
        public void Configure(EntityTypeBuilder<StatementSubsystem> entity)
        {
            entity.Property(e => e.ID).HasColumnName("ID");

            entity.Property(e => e.StatementID).HasColumnName("StatementID");

            entity.Property(e => e.SubsystemID).HasColumnName("SubsystemID");

            entity.HasOne(d => d.Statement)
                .WithMany(p => p.StatementSubsystem)
                .HasForeignKey(d => d.StatementID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StatementSubsystem_Statement");

            entity.HasOne(d => d.Subsystem)
                .WithMany(p => p.StatementSubsystem)
                .HasForeignKey(d => d.SubsystemID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_StatementSubsystem_Subsystem");
        }
    }
}
