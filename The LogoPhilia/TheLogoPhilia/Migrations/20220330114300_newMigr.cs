using Microsoft.EntityFrameworkCore.Migrations;

namespace The_LogoPhilia.Migrations
{
    public partial class newMigr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdministratorType",
                table: "ApplicationAdministrators",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdministratorType",
                table: "ApplicationAdministrators");
        }
    }
}
