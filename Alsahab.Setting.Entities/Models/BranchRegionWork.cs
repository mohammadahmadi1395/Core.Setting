using System;
using System.Collections.Generic;
using Alsahab.Setting.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alsahab.Setting.Entities.Models
{
    public class BranchRegionWork : BaseEntity<BranchRegionWork, BranchRegionWorkDTO, long>
    {
        // public long Id { get; set; }
        public long BranchID { get; set; }
        public long ZoneID { get; set; }
        //public bool? IsDeleted { get; set; }
        //public DateTime CreateDate { get; set; }

        public Branch Branch { get; set; }
        public Zone Zone { get; set; }
    }
    public class BranchRegionWorkConfiguration : IEntityTypeConfiguration<BranchRegionWork>
    {
        public void Configure(EntityTypeBuilder<BranchRegionWork> entity)
        {
            entity.Property(e => e.ID).HasColumnName("ID");
            entity.Property(e => e.BranchID).HasColumnName("BranchID");
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.ZoneID).HasColumnName("ZoneID");
            entity.HasOne(d => d.Branch)
                .WithMany(p => p.BranchRegionWork)
                .HasForeignKey(d => d.BranchID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BranchRegionWork_Branch");
            entity.HasOne(d => d.Zone)
                .WithMany(p => p.BranchRegionWork)
                .HasForeignKey(d => d.ZoneID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_BranchRegionWork_Zone");
        }
    }
}
