using Microsoft.EntityFrameworkCore.Migrations;

namespace Alsahab.Setting.Data.Migrations
{
    public partial class branchaddressidisnotrequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "BranchAddressID",
                table: "Branches",
                nullable: true,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "BranchAddressID",
                table: "Branches",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
