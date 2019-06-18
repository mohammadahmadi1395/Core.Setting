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
    [Migration("20190618031424_change_database")]
    partial class change_database
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
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ID")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("BranchAddressId")
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

                    b.Property<long?>("HeadPersonId")
                        .HasColumnName("HeadPersonID");

                    b.Property<bool>("IsCentral");

                    b.Property<bool?>("IsDeleted");

                    b.Property<long?>("LeftIndex");

                    b.Property<string>("OldCode")
                        .HasMaxLength(50);

                    b.Property<long?>("ParentId")
                        .HasColumnName("ParentID");

                    b.Property<long?>("RightIndex");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("Alsahab.Setting.Entities.Models.Branch", b =>
                {
                    b.HasOne("Alsahab.Setting.Entities.Models.Branch", "IdNavigation")
                        .WithOne("InverseIdNavigation")
                        .HasForeignKey("Alsahab.Setting.Entities.Models.Branch", "Id")
                        .HasConstraintName("FK_Branch_Branch");
                });
#pragma warning restore 612, 618
        }
    }
}
