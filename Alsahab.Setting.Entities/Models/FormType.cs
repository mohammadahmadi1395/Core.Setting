using System;
using System.Collections.Generic;
using Alsahab.Setting.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Alsahab.Setting.Entities.Models
{
    public class FormType : BaseEntity<FormType, FormTypeDTO, long>
    {
        public string Title { get; set; }
        public int? EnumID { get; set; }
        public long SubSystemID { get; set; }
        public string PublicCode { get; set; }
        public string Comment { get; set; }
        //public bool IsDeleted { get; set; }
        //public DateTime CreateDate { get; set; }

        public Subsystem SubSystem { get; set; }
        public ICollection<RuleTag> RuleTag { get; set; }
    }
    public class FormTypeConfiguration : IEntityTypeConfiguration<FormType>
    {
        public void Configure(EntityTypeBuilder<FormType> entity)
        {
            entity.Property(e => e.ID).HasColumnName("ID");

            entity.Property(e => e.CreateDate).HasColumnType("datetime");

            entity.Property(e => e.EnumID).HasColumnName("EnumID");

            entity.Property(e => e.PublicCode)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(e => e.SubSystemID).HasColumnName("SubSystemID");

            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(50);

            entity.HasOne(d => d.SubSystem)
                .WithMany(p => p.FormType)
                .HasForeignKey(d => d.SubSystemID)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FormType_Subsystem");
        }
    }

}
