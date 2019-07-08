using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Alsahab.Setting.DL.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GeneratedForms",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PublicCode = table.Column<string>(maxLength: 50, nullable: false),
                    PrivateCode = table.Column<string>(maxLength: 50, nullable: false),
                    SubsystemID = table.Column<long>(nullable: false),
                    UniqeCode = table.Column<long>(nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
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
                    UserID = table.Column<long>(nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    EntityID = table.Column<int>(nullable: false),
                    ActionTypeID = table.Column<int>(nullable: false),
                    RecordID = table.Column<long>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    GroupID = table.Column<long>(nullable: true),
                    RegistrantPersonID = table.Column<long>(nullable: true),
                    RegistrantPersonFullName = table.Column<string>(maxLength: 100, nullable: true),
                    GroupName = table.Column<string>(maxLength: 50, nullable: true)
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
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ParentID = table.Column<long>(nullable: true),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    LeftIndex = table.Column<long>(nullable: true),
                    RightIndex = table.Column<long>(nullable: true),
                    Depth = table.Column<long>(nullable: true),
                    OldCode = table.Column<string>(maxLength: 50, nullable: true)
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
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    IsDefault = table.Column<bool>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false)
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
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false)
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
                    TagName = table.Column<string>(maxLength: 50, nullable: false),
                    PersianText = table.Column<string>(maxLength: 50, nullable: false),
                    EnglishText = table.Column<string>(maxLength: 50, nullable: false),
                    ArabicText = table.Column<string>(maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true)
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
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    ShortName = table.Column<string>(maxLength: 50, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    Description = table.Column<string>(maxLength: 50, nullable: false),
                    IsSystem = table.Column<bool>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    RunOrder = table.Column<int>(nullable: true),
                    IsPart = table.Column<bool>(nullable: false)
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
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false)
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
                    ParentID = table.Column<long>(nullable: true),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    LeftIndex = table.Column<long>(nullable: true),
                    RightIndex = table.Column<long>(nullable: true),
                    Depth = table.Column<long>(nullable: true),
                    OldCode = table.Column<string>(maxLength: 50, nullable: true)
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
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    EnumID = table.Column<int>(nullable: true),
                    SubSystemID = table.Column<long>(nullable: false),
                    PublicCode = table.Column<string>(maxLength: 50, nullable: false),
                    Coment = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false)
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
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsSystem = table.Column<bool>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    SubsystemID = table.Column<long>(nullable: false),
                    Description = table.Column<string>(maxLength: 50, nullable: true)
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
                    ZoneID = table.Column<long>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Latitude = table.Column<double>(nullable: true),
                    Longitude = table.Column<double>(nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
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
                name: "RuleTags",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RuleID = table.Column<long>(nullable: false),
                    FormTypeID = table.Column<long>(nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    ID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ParentID = table.Column<long>(nullable: true),
                    BranchAddressID = table.Column<long>(nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: true),
                    HeadPersonID = table.Column<long>(nullable: true),
                    BranchPhoneNo = table.Column<string>(maxLength: 50, nullable: true),
                    BranchEmail = table.Column<string>(maxLength: 50, nullable: true),
                    IsCentral = table.Column<bool>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsDeleted = table.Column<bool>(nullable: true),
                    LeftIndex = table.Column<long>(nullable: true),
                    RightIndex = table.Column<long>(nullable: true),
                    Depth = table.Column<long>(nullable: true),
                    OldCode = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Branch_BranchAddress",
                        column: x => x.BranchAddressID,
                        principalTable: "BranchAddresses",
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
                    ZoneID = table.Column<long>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_BranchAddresses_ZoneID",
                table: "BranchAddresses",
                column: "ZoneID");

            migrationBuilder.CreateIndex(
                name: "IX_Branches_BranchAddressID",
                table: "Branches",
                column: "BranchAddressID");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "Branches");

            migrationBuilder.DropTable(
                name: "FormTypes");

            migrationBuilder.DropTable(
                name: "Rules");

            migrationBuilder.DropTable(
                name: "Statements");

            migrationBuilder.DropTable(
                name: "BranchAddresses");

            migrationBuilder.DropTable(
                name: "Subsystems");

            migrationBuilder.DropTable(
                name: "Zones");
        }
    }
}
