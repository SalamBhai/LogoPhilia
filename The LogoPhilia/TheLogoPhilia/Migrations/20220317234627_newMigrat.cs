using Microsoft.EntityFrameworkCore.Migrations;

namespace The_LogoPhilia.Migrations
{
    public partial class newMigrat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WordAlternatePronunciation",
                table: "Words");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WordAlternatePronunciation",
                table: "Words",
                type: "text",
                nullable: true);
        }
    }
}
