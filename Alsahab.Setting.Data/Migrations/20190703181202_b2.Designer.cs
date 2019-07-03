﻿// <auto-generated />
using System;
using Alsahab.Setting.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Alsahab.Setting.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190703181202_b2")]
    partial class b2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Alsahab.Setting.Entities.Models.Branch", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("BranchAddressID")
                        .HasColumnName("BranchAddressID");

                    b.Property<string>("BranchEmail")
                        .HasMaxLength(50);

                    b.Property<string>("BranchPhoneNo")
                        .HasMaxLength(50);

                    b.Property<string>("Code")
                        .HasMaxLength(50);

                    b.Property<string>("Comment");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<long?>("Depth");

                    b.Property<long?>("HeadPersonID")
                        .HasColumnName("HeadPersonID");

                    b.Property<bool>("IsCentral");

                    b.Property<bool>("IsDeleted");

                    b.Property<long?>("LeftIndex");

                    b.Property<string>("OldCode")
                        .HasMaxLength(50);

                    b.Property<long?>("ParentID")
                        .HasColumnName("ParentID");

                    b.Property<long?>("RightIndex");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.HasIndex("BranchAddressID");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("Alsahab.Setting.Entities.Models.BranchAddress", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsDeleted");

                    b.Property<double?>("Latitude");

                    b.Property<double?>("Longitude");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime");

                    b.Property<long>("ZoneID")
                        .HasColumnName("ZoneID");

                    b.HasKey("ID");

                    b.HasIndex("ZoneID");

                    b.ToTable("BranchAddresses");
                });

            modelBuilder.Entity("Alsahab.Setting.Entities.Models.BranchRegionWork", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long>("BranchID")
                        .HasColumnName("BranchID");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsDeleted");

                    b.Property<long>("ZoneID")
                        .HasColumnName("ZoneID");

                    b.HasKey("ID");

                    b.HasIndex("BranchID");

                    b.HasIndex("ZoneID");

                    b.ToTable("BranchRegionWorks");
                });

            modelBuilder.Entity("Alsahab.Setting.Entities.Models.FormType", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<int?>("EnumID")
                        .HasColumnName("EnumID");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("PublicCode")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<long>("SubSystemID")
                        .HasColumnName("SubSystemID");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.HasIndex("SubSystemID");

                    b.ToTable("FormTypes");
                });

            modelBuilder.Entity("Alsahab.Setting.Entities.Models.GeneratedForm", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("PrivateCode")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("PublicCode")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<long>("SubsystemId")
                        .HasColumnName("SubsystemID");

                    b.Property<long>("UniqeCode");

                    b.HasKey("ID");

                    b.ToTable("GeneratedForms");
                });

            modelBuilder.Entity("Alsahab.Setting.Entities.Models.Log", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ActionTypeID")
                        .HasColumnName("ActionTypeID");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<int>("EntityID")
                        .HasColumnName("EntityID");

                    b.Property<long?>("GroupID")
                        .HasColumnName("GroupID");

                    b.Property<string>("GroupName")
                        .HasMaxLength(50);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Message");

                    b.Property<long?>("RecordID")
                        .HasColumnName("RecordID");

                    b.Property<string>("RegistrantPersonFullName")
                        .HasMaxLength(100);

                    b.Property<long?>("RegistrantPersonID")
                        .HasColumnName("RegistrantPersonID");

                    b.Property<long>("UserID")
                        .HasColumnName("UserID");

                    b.HasKey("ID");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("Alsahab.Setting.Entities.Models.OrganizationalChart", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<long?>("Depth");

                    b.Property<bool>("IsDeleted");

                    b.Property<long?>("LeftIndex");

                    b.Property<string>("OldCode")
                        .HasMaxLength(50);

                    b.Property<long?>("ParentId")
                        .HasColumnName("ParentID");

                    b.Property<long?>("RightIndex");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.HasIndex("ParentId");

                    b.ToTable("OrganizationalCharts");
                });

            modelBuilder.Entity("Alsahab.Setting.Entities.Models.OrganizationType", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("OrganizationTypes");
                });

            modelBuilder.Entity("Alsahab.Setting.Entities.Models.Prefix", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("Prefixes");
                });

            modelBuilder.Entity("Alsahab.Setting.Entities.Models.Rule", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("Type");

                    b.HasKey("ID");

                    b.ToTable("Rules");
                });

            modelBuilder.Entity("Alsahab.Setting.Entities.Models.RuleTag", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<long>("FormTypeID")
                        .HasColumnName("FormTypeID");

                    b.Property<bool>("IsDeleted");

                    b.Property<long>("RuleID")
                        .HasColumnName("RuleID");

                    b.HasKey("ID");

                    b.HasIndex("FormTypeID");

                    b.HasIndex("RuleID");

                    b.ToTable("RuleTags");
                });

            modelBuilder.Entity("Alsahab.Setting.Entities.Models.Statement", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ArabicText")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<string>("EnglishText")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("PersianText")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("Statements");
                });

            modelBuilder.Entity("Alsahab.Setting.Entities.Models.StatementSubsystem", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate");

                    b.Property<bool>("IsDeleted");

                    b.Property<long>("StatementID")
                        .HasColumnName("StatementID");

                    b.Property<long>("SubsystemID")
                        .HasColumnName("SubsystemID");

                    b.HasKey("ID");

                    b.HasIndex("StatementID");

                    b.HasIndex("SubsystemID");

                    b.ToTable("StatementSubsystems");
                });

            modelBuilder.Entity("Alsahab.Setting.Entities.Models.Subpart", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasMaxLength(50);

                    b.Property<bool>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsSystem");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<long>("SubsystemID")
                        .HasColumnName("SubsystemID");

                    b.HasKey("ID");

                    b.HasIndex("SubsystemID");

                    b.ToTable("Subparts");
                });

            modelBuilder.Entity("Alsahab.Setting.Entities.Models.Subsystem", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<bool?>("IsActive");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsPart");

                    b.Property<bool?>("IsSystem");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("RunOrder");

                    b.Property<string>("ShortName")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.ToTable("Subsystems");
                });

            modelBuilder.Entity("Alsahab.Setting.Entities.Models.Zone", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("Comment");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime");

                    b.Property<long?>("Depth");

                    b.Property<bool>("IsDeleted");

                    b.Property<long?>("LeftIndex");

                    b.Property<string>("OldCode")
                        .HasMaxLength(50);

                    b.Property<long?>("ParentID")
                        .HasColumnName("ParentID");

                    b.Property<long?>("RightIndex");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("Type");

                    b.HasKey("ID");

                    b.HasIndex("ParentID");

                    b.ToTable("Zones");
                });

            modelBuilder.Entity("Alsahab.Setting.Entities.Models.Branch", b =>
                {
                    b.HasOne("Alsahab.Setting.Entities.Models.BranchAddress", "BranchAddress")
                        .WithMany("Branch")
                        .HasForeignKey("BranchAddressID")
                        .HasConstraintName("FK_Branch_BranchAddress");

                    b.HasOne("Alsahab.Setting.Entities.Models.Branch", "IDNavigation")
                        .WithOne("InverseIDNavigation")
                        .HasForeignKey("Alsahab.Setting.Entities.Models.Branch", "ID")
                        .HasConstraintName("FK_Branch_Branch");
                });

            modelBuilder.Entity("Alsahab.Setting.Entities.Models.BranchAddress", b =>
                {
                    b.HasOne("Alsahab.Setting.Entities.Models.Zone", "Zone")
                        .WithMany("BranchAddress")
                        .HasForeignKey("ZoneID")
                        .HasConstraintName("FK_BranchAddress_Zone");
                });

            modelBuilder.Entity("Alsahab.Setting.Entities.Models.BranchRegionWork", b =>
                {
                    b.HasOne("Alsahab.Setting.Entities.Models.Branch", "Branch")
                        .WithMany("BranchRegionWork")
                        .HasForeignKey("BranchID")
                        .HasConstraintName("FK_BranchRegionWork_Branch");

                    b.HasOne("Alsahab.Setting.Entities.Models.Zone", "Zone")
                        .WithMany("BranchRegionWork")
                        .HasForeignKey("ZoneID")
                        .HasConstraintName("FK_BranchRegionWork_Zone");
                });

            modelBuilder.Entity("Alsahab.Setting.Entities.Models.FormType", b =>
                {
                    b.HasOne("Alsahab.Setting.Entities.Models.Subsystem", "SubSystem")
                        .WithMany("FormType")
                        .HasForeignKey("SubSystemID")
                        .HasConstraintName("FK_FormType_Subsystem");
                });

            modelBuilder.Entity("Alsahab.Setting.Entities.Models.OrganizationalChart", b =>
                {
                    b.HasOne("Alsahab.Setting.Entities.Models.OrganizationalChart", "Parent")
                        .WithMany("InverseParent")
                        .HasForeignKey("ParentId")
                        .HasConstraintName("FK_OrganizationalChart_OrganizationalChart");
                });

            modelBuilder.Entity("Alsahab.Setting.Entities.Models.RuleTag", b =>
                {
                    b.HasOne("Alsahab.Setting.Entities.Models.FormType", "FormType")
                        .WithMany("RuleTag")
                        .HasForeignKey("FormTypeID")
                        .HasConstraintName("FK_RuleTag_FormType");

                    b.HasOne("Alsahab.Setting.Entities.Models.Rule", "Rule")
                        .WithMany("RuleTag")
                        .HasForeignKey("RuleID")
                        .HasConstraintName("FK_RuleTag_Rule");
                });

            modelBuilder.Entity("Alsahab.Setting.Entities.Models.StatementSubsystem", b =>
                {
                    b.HasOne("Alsahab.Setting.Entities.Models.Statement", "Statement")
                        .WithMany("StatementSubsystem")
                        .HasForeignKey("StatementID")
                        .HasConstraintName("FK_StatementSubsystem_Statement");

                    b.HasOne("Alsahab.Setting.Entities.Models.Subsystem", "Subsystem")
                        .WithMany("StatementSubsystem")
                        .HasForeignKey("SubsystemID")
                        .HasConstraintName("FK_StatementSubsystem_Subsystem");
                });

            modelBuilder.Entity("Alsahab.Setting.Entities.Models.Subpart", b =>
                {
                    b.HasOne("Alsahab.Setting.Entities.Models.Subsystem", "Subsystem")
                        .WithMany("Subpart")
                        .HasForeignKey("SubsystemID")
                        .HasConstraintName("FK_Subpart_Subsystem");
                });

            modelBuilder.Entity("Alsahab.Setting.Entities.Models.Zone", b =>
                {
                    b.HasOne("Alsahab.Setting.Entities.Models.Zone", "Parent")
                        .WithMany("InverseParent")
                        .HasForeignKey("ParentID")
                        .HasConstraintName("FK_Zone_Zone");
                });
#pragma warning restore 612, 618
        }
    }
}
