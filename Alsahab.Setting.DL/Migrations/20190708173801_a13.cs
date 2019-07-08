using Microsoft.EntityFrameworkCore.Migrations;

namespace Alsahab.Setting.DL.Migrations
{
    public partial class a13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Prefixes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Prefixes");
        }
    }
}
