using Microsoft.EntityFrameworkCore.Migrations;

namespace The_LogoPhilia.Migrations
{
    public partial class newMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BritishOrAmerican",
                table: "Words",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BritishOrAmerican",
                table: "Words");
        }
    }
}
