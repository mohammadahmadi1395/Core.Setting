using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alsahab.Setting.Entities.Models
{
    public class Statement : BaseEntity
    {
        // public long Id { get; set; }
        public string TagName { get; set; }
        public string PersianText { get; set; }
        public string EnglishText { get; set; }
        public string ArabicText { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreateDate { get; set; }

        public ICollection<StatementSubsystem> StatementSubsystem { get; set; }
    }

    public class StatementConfiguration : IEntityTypeConfiguration<Statement>
    {
        public void Configure(EntityTypeBuilder<Statement> entity)
        {
            entity.Property(e => e.Id).HasColumnName("ID");

            entity.Property(e => e.ArabicText)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.CreateDate).HasColumnType("datetime");

            entity.Property(e => e.EnglishText)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.PersianText)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.TagName)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
