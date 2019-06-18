using System;
using System.Collections.Generic;
using Alsahab.Setting.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alsahab.Setting.Entities.Models
{
    public class Branch : BaseEntity<BranchDTO>
    {
        public long? ParentId { get; set; }
        public long? BranchAddressId { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public long? HeadPersonId { get; set; }
        public string BranchPhoneNo { get; set; }
        public string BranchEmail { get; set; }
        public bool IsCentral { get; set; }
        public string Comment { get; set; }
        public DateTime CreateDate { get; set; }
        public bool? IsDeleted { get; set; }
        public long? LeftIndex { get; set; }
        public long? RightIndex { get; set; }
        public long? Depth { get; set; }
        public string OldCode { get; set; }

        //TODO
        // public BranchAddress BranchAddress { get; set; }
        public Branch IdNavigation { get; set; }
        public Branch InverseIdNavigation { get; set; }
        //TODO
        // public ICollection<BranchRegionWork> BranchRegionWork { get; set; }
    }


    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> entity)
        {
            entity.Property(e => e.Id).HasColumnName("ID").ValueGeneratedOnAdd();
            entity.Property(e => e.BranchAddressId).HasColumnName("BranchAddressID");
            entity.Property(e => e.BranchEmail).HasMaxLength(50);
            entity.Property(e => e.BranchPhoneNo).HasMaxLength(50);
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.HeadPersonId).HasColumnName("HeadPersonID");
            entity.Property(e => e.OldCode).HasMaxLength(50);
            entity.Property(e => e.ParentId).HasColumnName("ParentID");
            entity.Property(e => e.Title).IsRequired().HasMaxLength(50);
            //TODO
            // entity.HasOne(d => d.BranchAddress).WithMany(p => p.Branch).HasForeignKey(d => d.BranchAddressId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Branch_BranchAddress");
            entity.HasOne(d => d.IdNavigation).WithOne(p => p.InverseIdNavigation).HasForeignKey<Branch>(d => d.Id).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Branch_Branch");
        }
    }
}
