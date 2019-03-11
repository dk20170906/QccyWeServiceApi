using Microsoft.EntityFrameworkCore.Migrations;

namespace QccyWeServiceApi.Migrations
{
    public partial class migrations12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Access_token",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remember",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Vercode",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Access_token",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Remember",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Vercode",
                table: "Users");
        }
    }
}
