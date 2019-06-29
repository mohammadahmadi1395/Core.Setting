using System;
using System.Collections.Generic;
using Alsahab.Setting.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alsahab.Setting.Entities.Models
{
    public class Branch : BaseEntity<Branch, BranchDTO, long>
    {
        public long? ParentID { get; set; }
        public long? BranchAddressID { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public long? HeadPersonID { get; set; }
        public string BranchPhoneNo { get; set; }
        public string BranchEmail { get; set; }
        public bool IsCentral { get; set; }
        public string Comment { get; set; }
        public long? LeftIndex { get; set; }
        public long? RightIndex { get; set; }
        public long? Depth { get; set; }
        public string OldCode { get; set; }

        public BranchAddress BranchAddress { get; set; }
        public Branch IDNavigation { get; set; }
        public Branch InverseIDNavigation { get; set; }
        public ICollection<BranchRegionWork> BranchRegionWork { get; set; }

        // public override void CustomMappings(AutoMapper.IMappingExpression<BranchDTO, Branch> mapping)
        // {
        //     // mapping.ForMember(
        //     //     dest => dest.ParentId,
        //     //     config => config.MapFrom(src => $"{src.ParentID} ({src.Category.Name})")
        //     // );
        // }
    }


    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> entity)
        {
            entity.Property(e => e.ID).HasColumnName("ID").ValueGeneratedOnAdd();
            entity.Property(e => e.BranchAddressID).HasColumnName("BranchAddressID");
            entity.Property(e => e.BranchEmail).HasMaxLength(50);
            entity.Property(e => e.BranchPhoneNo).HasMaxLength(50);
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.HeadPersonID).HasColumnName("HeadPersonID");
            entity.Property(e => e.OldCode).HasMaxLength(50);
            entity.Property(e => e.ParentID).HasColumnName("ParentID");
            entity.Property(e => e.Title).IsRequired().HasMaxLength(50);
            entity.HasOne(d => d.BranchAddress).WithMany(p => p.Branch).HasForeignKey(d => d.BranchAddressID).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Branch_BranchAddress");
            entity.HasOne(d => d.IDNavigation).WithOne(p => p.InverseIDNavigation).HasForeignKey<Branch>(d => d.ID).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Branch_Branch");
        }
    }
}
