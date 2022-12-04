using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaLibrary.Migrations
{
    public partial class fixStatusAppeal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Appeal");

            migrationBuilder.AddColumn<bool>(
                name: "IsAnswered",
                table: "Appeal",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAnswered",
                table: "Appeal");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Appeal",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
