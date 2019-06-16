using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alsahab.Setting.Entities.Models
{
    public class Rule : BaseEntity
    {
        // public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreateDate { get; set; }

        public ICollection<RuleTag> RuleTag { get; set; }
    }

    public class RuleConfiguration : IEntityTypeConfiguration<Rule>
    {
        public void Configure(EntityTypeBuilder<Rule> entity)
        {
            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");

            entity.Property(e => e.Description).IsRequired();

            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
