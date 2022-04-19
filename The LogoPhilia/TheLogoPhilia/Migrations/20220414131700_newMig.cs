using Microsoft.EntityFrameworkCore.Migrations;

namespace The_LogoPhilia.Migrations
{
    public partial class newMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasNewsLetterInterest",
                table: "ApplicationUsers",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ApplicationUserPosts",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasNewsLetterInterest",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "ApplicationUserPosts");
        }
    }
}
