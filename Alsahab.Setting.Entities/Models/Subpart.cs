using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alsahab.Setting.Entities.Models
{
    public class Subpart : BaseEntity
    {
        // public long Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsSystem { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreateDate { get; set; }
        public long SubsystemId { get; set; }
        public string Description { get; set; }

        public Subsystem Subsystem { get; set; }
    }

    public class SubpartConfiguration : IEntityTypeConfiguration<Subpart>
    {
        public void Configure(EntityTypeBuilder<Subpart> entity)
        {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SubsystemId).HasColumnName("SubsystemID");

                entity.HasOne(d => d.Subsystem)
                    .WithMany(p => p.Subpart)
                    .HasForeignKey(d => d.SubsystemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Subpart_Subsystem");
        }
    }
}
