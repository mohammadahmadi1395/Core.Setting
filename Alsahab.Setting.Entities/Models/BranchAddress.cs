// using System;
// using System.Collections.Generic;
// using Alsahab.Setting.DTO;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;

// namespace Alsahab.Setting.Entities.Models
// {
//     public class BranchAddress : BaseEntity<BranchAddressDTO>
//     {
//         // public long Id { get; set; }
//         public long ZoneId { get; set; }
//         public string Address { get; set; }
//         public DateTime? StartDate { get; set; }
//         public DateTime? EndDate { get; set; }
//         public double? Latitude { get; set; }
//         public double? Longitude { get; set; }
//         public DateTime CreateDate { get; set; }
//         public bool IsDeleted { get; set; }

//         public Zone Zone { get; set; }
//         public ICollection<Branch> Branch { get; set; }
//     }

//     public class BranchAddressConfiguration : IEntityTypeConfiguration<BranchAddress>
//     {
//         public void Configure(EntityTypeBuilder<BranchAddress> entity)
//         {
//             entity.Property(e => e.Id).HasColumnName("ID");
//             entity.Property(e => e.CreateDate).HasColumnType("datetime");
//             entity.Property(e => e.EndDate).HasColumnType("datetime");
//             entity.Property(e => e.StartDate).HasColumnType("datetime");
//             entity.Property(e => e.ZoneId).HasColumnName("ZoneID");
//             entity.HasOne(d => d.Zone).WithMany(p => p.BranchAddress).HasForeignKey(d => d.ZoneId).OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_BranchAddress_Zone");
//         }
//     }

// }
