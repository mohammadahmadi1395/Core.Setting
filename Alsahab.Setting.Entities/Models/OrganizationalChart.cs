using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alsahab.Setting.Entities.Models
{
    public class OrganizationalChart : BaseEntity
    {
        // public long Id { get; set; }
        public string Title { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }
        public long? ParentId { get; set; }
        public string Code { get; set; }
        public long? LeftIndex { get; set; }
        public long? RightIndex { get; set; }
        public long? Depth { get; set; }
        public string OldCode { get; set; }

        public OrganizationalChart Parent { get; set; }
        public ICollection<OrganizationalChart> InverseParent { get; set; }
    }

    public class OrganizationalChartConfiguration : IEntityTypeConfiguration<OrganizationalChart>
    {
        public void Configure(EntityTypeBuilder<OrganizationalChart> entity)
        {
            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.Code).HasMaxLength(50);

            entity.Property(e => e.CreateDate).HasColumnType("datetime");

            entity.Property(e => e.OldCode).HasMaxLength(50);

            entity.Property(e => e.ParentId).HasColumnName("ParentID");

            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.Parent)
                .WithMany(p => p.InverseParent)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK_OrganizationalChart_OrganizationalChart");
        }
    }
}
