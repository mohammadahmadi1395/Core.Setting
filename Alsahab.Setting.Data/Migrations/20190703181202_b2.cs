using Microsoft.EntityFrameworkCore.Migrations;

namespace Alsahab.Setting.Data.Migrations
{
    public partial class b2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Coment",
                table: "FormTypes",
                newName: "Comment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "FormTypes",
                newName: "Coment");
        }
    }
}
