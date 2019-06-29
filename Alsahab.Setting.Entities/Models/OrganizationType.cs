using System;
using System.Collections.Generic;
using Alsahab.Setting.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alsahab.Setting.Entities.Models
{
    public class OrganizationType : BaseEntity<OrganizationType, OrganizationTypeDTO, long>
    {
        // public long Id { get; set; }
        public string Title { get; set; }
        //public bool IsDeleted { get; set; }
        //public DateTime CreateDate { get; set; }
    }

    public class TypeoforganizationConfiguration : IEntityTypeConfiguration<OrganizationType>
    {
        public void Configure(EntityTypeBuilder<OrganizationType> entity)
        {
            entity.Property(e => e.ID).HasColumnName("ID");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");

            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);
        }

    }
}
