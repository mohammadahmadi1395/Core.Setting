using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alsahab.Setting.Entities.Models
{
    public class RuleTag : BaseEntity
    {
        // public long Id { get; set; }
        public long RuleId { get; set; }
        public long FormTypeId { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsDeleted { get; set; }

        public FormType FormType { get; set; }
        public Rule Rule { get; set; }
    }

    public class RuleTagConfiguration : IEntityTypeConfiguration<RuleTag>
    {
        public void Configure(EntityTypeBuilder<RuleTag> entity)
        {
            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");

            entity.Property(e => e.FormTypeId).HasColumnName("FormTypeID");

            entity.Property(e => e.RuleId).HasColumnName("RuleID");

            entity.HasOne(d => d.FormType)
                .WithMany(p => p.RuleTag)
                .HasForeignKey(d => d.FormTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RuleTag_FormType");

            entity.HasOne(d => d.Rule)
                .WithMany(p => p.RuleTag)
                .HasForeignKey(d => d.RuleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RuleTag_Rule");
        }
    }
}
