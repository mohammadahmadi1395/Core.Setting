using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Alsahab.Setting.Data.Migrations
{
    public partial class change_database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Branch_BranchAddress",
                table: "Branches");

            migrationBuilder.DropTable(
                name: "BranchAddresses");

            migrationBuilder.DropTable(
                name: "BranchRegionWorks");

            migrationBuilder.DropTable(
                name: "GeneratedForms");

            migrationBuilder.DropTable(
                name: "Logs");

            migrationBuilder.DropTable(
                name: "OrganizationalCharts");

            migrationBuilder.DropTable(
                name: "Prefixes");

            migrationBuilder.DropTable(
                name: "RuleTags");

            migrationBuilder.DropTable(
                name: "StatementSubsystems");

            migrationBuilder.DropTable(
                name: "Subparts");

            migrationBuilder.DropTable(
                name: "Typeoforganizations");

            migrationBuilder.DropTable(
                name: "Zones");

            migrationBuilder.DropTable(
                name: "FormTypes");

            migrationBuilder.DropTable(
                name: "Rules");

            migrationBuilder.DropTable(
                name: "Statements");

            migrationBuilder.DropTable(
                name: "Subsystems");

            migrationBuilder.DropIndex(
                name: "IX_Branches_BranchAddressID",
                table: "Branches");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeneratedForms",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PrivateCode = table.Column<string>(maxLength: 50, nullable: false),
                    PublicCode = table.Column<string>(maxLength: 50, nullable: false),
                    SubsystemID = table.Column<long>(nullable: false),
                    UniqeCode = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneratedForms", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Logs",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ActionTypeID = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EntityID = table.Column<int>(nullable: false),
                    GroupID = table.Column<long>(nullable: true),
                    GroupName = table.Column<string>(maxLength: 50, nullable: true),
                    Message = table.Column<string>(nullable: true),
                    RecordID = table.Column<long>(nullable: true),
                    RegistrantPersonFullName = table.Column<string>(maxLength: 100, nullable: true),
                    RegistrantPersonID = table.Column<long>(nullable: true),
                    UserID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "OrganizationalCharts",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Depth = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LeftIndex = table.Column<long>(nullable: true),
                    OldCode = table.Column<string>(maxLength: 50, nullable: true),
                    ParentID = table.Column<long>(nullable: true),
                    RightIndex = table.Column<long>(nullable: true),
                    Title = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationalCharts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OrganizationalChart_OrganizationalChart",
                        column: x => x.ParentID,
                        principalTable: "OrganizationalCharts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prefixes",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsDefault = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prefixes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Rules",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Description = table.Column<string>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Statements",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArabicText = table.Column<string>(maxLength: 50, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EnglishText = table.Column<string>(maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: true),
                    PersianText = table.Column<string>(maxLength: 50, nullable: false),
                    TagName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statements", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Subsystems",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Description = table.Column<string>(maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    IsPart = table.Column<bool>(nullable: false),
                    IsSystem = table.Column<bool>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    RunOrder = table.Column<int>(nullable: true),
                    ShortName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subsystems", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Typeoforganizations",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Typeoforganizations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Zones",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Depth = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LeftIndex = table.Column<long>(nullable: true),
                    OldCode = table.Column<string>(maxLength: 50, nullable: true),
                    ParentID = table.Column<long>(nullable: true),
                    RightIndex = table.Column<long>(nullable: true),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zones", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Zone_Zone",
                        column: x => x.ParentID,
                        principalTable: "Zones",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FormTypes",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Coment = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EnumID = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    PublicCode = table.Column<string>(maxLength: 50, nullable: false),
                    SubSystemID = table.Column<long>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormTypes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FormType_Subsystem",
                        column: x => x.SubSystemID,
                        principalTable: "Subsystems",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StatementSubsystems",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StatementID = table.Column<long>(nullable: false),
                    SubsystemID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatementSubsystems", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StatementSubsystem_Statement",
                        column: x => x.StatementID,
                        principalTable: "Statements",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StatementSubsystem_Subsystem",
                        column: x => x.SubsystemID,
                        principalTable: "Subsystems",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Subparts",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Description = table.Column<string>(maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsSystem = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    SubsystemID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subparts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Subpart_Subsystem",
                        column: x => x.SubsystemID,
                        principalTable: "Subsystems",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BranchAddresses",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Latitude = table.Column<double>(nullable: true),
                    Longitude = table.Column<double>(nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ZoneID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchAddresses", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BranchAddress_Zone",
                        column: x => x.ZoneID,
                        principalTable: "Zones",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BranchRegionWorks",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BranchID = table.Column<long>(nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsDeleted = table.Column<bool>(nullable: true),
                    ZoneID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchRegionWorks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BranchRegionWork_Branch",
                        column: x => x.BranchID,
                        principalTable: "Branches",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BranchRegionWork_Zone",
                        column: x => x.ZoneID,
                        principalTable: "Zones",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RuleTags",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    FormTypeID = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    RuleID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RuleTags", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RuleTag_FormType",
                        column: x => x.FormTypeID,
                        principalTable: "FormTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RuleTag_Rule",
                        column: x => x.RuleID,
                        principalTable: "Rules",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Branches_BranchAddressID",
                table: "Branches",
                column: "BranchAddressID");

            migrationBuilder.CreateIndex(
                name: "IX_BranchAddresses_ZoneID",
                table: "BranchAddresses",
                column: "ZoneID");

            migrationBuilder.CreateIndex(
                name: "IX_BranchRegionWorks_BranchID",
                table: "BranchRegionWorks",
                column: "BranchID");

            migrationBuilder.CreateIndex(
                name: "IX_BranchRegionWorks_ZoneID",
                table: "BranchRegionWorks",
                column: "ZoneID");

            migrationBuilder.CreateIndex(
                name: "IX_FormTypes_SubSystemID",
                table: "FormTypes",
                column: "SubSystemID");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationalCharts_ParentID",
                table: "OrganizationalCharts",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_RuleTags_FormTypeID",
                table: "RuleTags",
                column: "FormTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_RuleTags_RuleID",
                table: "RuleTags",
                column: "RuleID");

            migrationBuilder.CreateIndex(
                name: "IX_StatementSubsystems_StatementID",
                table: "StatementSubsystems",
                column: "StatementID");

            migrationBuilder.CreateIndex(
                name: "IX_StatementSubsystems_SubsystemID",
                table: "StatementSubsystems",
                column: "SubsystemID");

            migrationBuilder.CreateIndex(
                name: "IX_Subparts_SubsystemID",
                table: "Subparts",
                column: "SubsystemID");

            migrationBuilder.CreateIndex(
                name: "IX_Zones_ParentID",
                table: "Zones",
                column: "ParentID");

            migrationBuilder.AddForeignKey(
                name: "FK_Branch_BranchAddress",
                table: "Branches",
                column: "BranchAddressID",
                principalTable: "BranchAddresses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
