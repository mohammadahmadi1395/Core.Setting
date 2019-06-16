using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Alsahab.Setting.Data.Models
{
    public partial class SettingContext : DbContext
    {
        public SettingContext()
        {
        }

        public SettingContext(DbContextOptions<SettingContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<Branch> Branch { get; set; }
        public virtual DbSet<BranchAddress> BranchAddress { get; set; }
        public virtual DbSet<BranchRegionWork> BranchRegionWork { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<ExchangeRate> ExchangeRate { get; set; }
        public virtual DbSet<FormType> FormType { get; set; }
        public virtual DbSet<GeneratedForm> GeneratedForm { get; set; }
        public virtual DbSet<Log> Log { get; set; }
        public virtual DbSet<OrganizationalChart> OrganizationalChart { get; set; }
        public virtual DbSet<Prefix> Prefix { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<RegionAgent> RegionAgent { get; set; }
        public virtual DbSet<Rule> Rule { get; set; }
        public virtual DbSet<RuleTag> RuleTag { get; set; }
        public virtual DbSet<Sector> Sector { get; set; }
        public virtual DbSet<Statement> Statement { get; set; }
        public virtual DbSet<StatementSubsystem> StatementSubsystem { get; set; }
        public virtual DbSet<Subpart> Subpart { get; set; }
        public virtual DbSet<Subsystem> Subsystem { get; set; }
        public virtual DbSet<Typeoforganization> Typeoforganization { get; set; }
        public virtual DbSet<Zone> Zone { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=192.168.1.7;Database=Setting;Trusted_Connection=True;User Id=sa;Password=admin@123;Integrated Security=false;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Area>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Area)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Area_City");
            });

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.BranchAddressId).HasColumnName("BranchAddressID");

                entity.Property(e => e.BranchEmail).HasMaxLength(50);

                entity.Property(e => e.BranchPhoneNo).HasMaxLength(50);

                entity.Property(e => e.Code).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.HeadPersonId).HasColumnName("HeadPersonID");

                entity.Property(e => e.OldCode).HasMaxLength(50);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.BranchAddress)
                    .WithMany(p => p.Branch)
                    .HasForeignKey(d => d.BranchAddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Branch_BranchAddress");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.InverseIdNavigation)
                    .HasForeignKey<Branch>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Branch_Branch");
            });

            modelBuilder.Entity<BranchAddress>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.ZoneId).HasColumnName("ZoneID");

                entity.HasOne(d => d.Zone)
                    .WithMany(p => p.BranchAddress)
                    .HasForeignKey(d => d.ZoneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BranchAddress_Zone");
            });

            modelBuilder.Entity<BranchRegionWork>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.ZoneId).HasColumnName("ZoneID");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.BranchRegionWork)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BranchRegionWork_Branch");

                entity.HasOne(d => d.Zone)
                    .WithMany(p => p.BranchRegionWork)
                    .HasForeignKey(d => d.ZoneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BranchRegionWork_Zone");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CountryId).HasColumnName("CountryID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.City)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_City_Country");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ShortName).HasMaxLength(3);
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Symbol)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ExchangeRate>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.FromCurrencyId).HasColumnName("FromCurrencyID");

                entity.Property(e => e.ToCurrencyId).HasColumnName("ToCurrencyID");

                entity.Property(e => e.Year).HasColumnType("datetime");

                entity.HasOne(d => d.FromCurrency)
                    .WithMany(p => p.ExchangeRateFromCurrency)
                    .HasForeignKey(d => d.FromCurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExchangeRate_Currency");

                entity.HasOne(d => d.ToCurrency)
                    .WithMany(p => p.ExchangeRateToCurrency)
                    .HasForeignKey(d => d.ToCurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExchangeRate_Currency1");
            });

            modelBuilder.Entity<FormType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.EnumId).HasColumnName("EnumID");

                entity.Property(e => e.PublicCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SubSystemId).HasColumnName("SubSystemID");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.SubSystem)
                    .WithMany(p => p.FormType)
                    .HasForeignKey(d => d.SubSystemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FormType_Subsystem");
            });

            modelBuilder.Entity<GeneratedForm>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.PrivateCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PublicCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SubsystemId).HasColumnName("SubsystemID");
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.ActionTypeId).HasColumnName("ActionTypeID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.EntityId).HasColumnName("EntityID");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.GroupName).HasMaxLength(50);

                entity.Property(e => e.RecordId).HasColumnName("RecordID");

                entity.Property(e => e.RegistrantPersonFullName).HasMaxLength(100);

                entity.Property(e => e.RegistrantPersonId).HasColumnName("RegistrantPersonID");

                entity.Property(e => e.UserId).HasColumnName("UserID");
            });

            modelBuilder.Entity<OrganizationalChart>(entity =>
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
            });

            modelBuilder.Entity<Prefix>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AreaId).HasColumnName("AreaID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Area)
                    .WithMany(p => p.Region)
                    .HasForeignKey(d => d.AreaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Region_Area");
            });

            modelBuilder.Entity<RegionAgent>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.RegionId).HasColumnName("RegionID");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.RegionAgent)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RegionAgent_Region");
            });

            modelBuilder.Entity<Rule>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<RuleTag>(entity =>
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
            });

            modelBuilder.Entity<Sector>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RegionId).HasColumnName("RegionID");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Sector)
                    .HasForeignKey(d => d.RegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sector_Region");
            });

            modelBuilder.Entity<Statement>(entity =>
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
            });

            modelBuilder.Entity<StatementSubsystem>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.StatementId).HasColumnName("StatementID");

                entity.Property(e => e.SubsystemId).HasColumnName("SubsystemID");

                entity.HasOne(d => d.Statement)
                    .WithMany(p => p.StatementSubsystem)
                    .HasForeignKey(d => d.StatementId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StatementSubsystem_Statement");

                entity.HasOne(d => d.Subsystem)
                    .WithMany(p => p.StatementSubsystem)
                    .HasForeignKey(d => d.SubsystemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StatementSubsystem_Subsystem");
            });

            modelBuilder.Entity<Subpart>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SubsystemId).HasColumnName("SubsystemID");

                entity.HasOne(d => d.Subsystem)
                    .WithMany(p => p.Subpart)
                    .HasForeignKey(d => d.SubsystemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Subpart_Subsystem");
            });

            modelBuilder.Entity<Subsystem>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ShortName).HasMaxLength(50);
            });

            modelBuilder.Entity<Typeoforganization>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Zone>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.OldCode).HasMaxLength(50);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Zone_Zone");
            });
        }
    }
}
