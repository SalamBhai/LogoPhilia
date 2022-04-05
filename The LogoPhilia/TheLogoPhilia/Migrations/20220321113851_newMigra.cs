using Microsoft.EntityFrameworkCore.Migrations;

namespace The_LogoPhilia.Migrations
{
    public partial class newMigra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NormalPassword",
                table: "Users",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NormalPassword",
                table: "Users");
        }
    }
}
