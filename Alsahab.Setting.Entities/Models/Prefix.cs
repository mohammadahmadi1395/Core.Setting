using System;
using System.Collections.Generic;
using Alsahab.Setting.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alsahab.Setting.Entities.Models
{
    public class Prefix : BaseEntity<Prefix, PrefixDTO, long>
    {
        // public long Id { get; set; }
        public string Title { get; set; }
        public bool IsDefault { get; set; }
    }

    public class PrefixConfiguration : IEntityTypeConfiguration<Prefix>
    {
        public void Configure(EntityTypeBuilder<Prefix> entity)
        {
            entity.Property(e => e.ID).HasColumnName("ID");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");

            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}
