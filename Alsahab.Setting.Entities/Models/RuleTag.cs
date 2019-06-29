using System;
using System.Collections.Generic;
using Alsahab.Setting.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alsahab.Setting.Entities.Models
{
    public class RuleTag : BaseEntity<RuleTag, RuleTagDTO, long>
    {
        // public long Id { get; set; }
        public long RuleID { get; set; }
        public long FormTypeID { get; set; }
        //public DateTime CreateDate { get; set; }
        //public bool IsDeleted { get; set; }

        public FormType FormType { get; set; }
        public Rule Rule { get; set; }
    }

    public class RuleTagConfiguration : IEntityTypeConfiguration<RuleTag>
    {
        public void Configure(EntityTypeBuilder<RuleTag> entity)
        {
            entity.Property(e => e.ID).HasColumnName("ID");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");

            entity.Property(e => e.FormTypeID).HasColumnName("FormTypeID");

            entity.Property(e => e.RuleID).HasColumnName("RuleID");

            entity.HasOne(d => d.FormType)
                .WithMany(p => p.RuleTag)
                .HasForeignKey(d => d.FormTypeID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RuleTag_FormType");

            entity.HasOne(d => d.Rule)
                .WithMany(p => p.RuleTag)
                .HasForeignKey(d => d.RuleID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RuleTag_Rule");
        }
    }
}
