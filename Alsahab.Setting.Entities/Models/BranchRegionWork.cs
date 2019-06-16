using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alsahab.Setting.Entities.Models
{
    public class BranchRegionWork : BaseEntity
    {
        // public long Id { get; set; }
        public long BranchId { get; set; }
        public long ZoneId { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }

        public Branch Branch { get; set; }
        public Zone Zone { get; set; }
    }
    public class BranchRegionWorkConfiguration : IEntityTypeConfiguration<BranchRegionWork>
    {
        public void Configure(EntityTypeBuilder<BranchRegionWork> entity)
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BranchId).HasColumnName("BranchID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.ZoneId).HasColumnName("ZoneID");
            entity.HasOne(d => d.Branch)
                .WithMany(p => p.BranchRegionWork)
                .HasForeignKey(d => d.BranchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BranchRegionWork_Branch");
            entity.HasOne(d => d.Zone)
                .WithMany(p => p.BranchRegionWork)
                .HasForeignKey(d => d.ZoneId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BranchRegionWork_Zone");
        }
    }
}
