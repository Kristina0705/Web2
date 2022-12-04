using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaLibrary.Migrations
{
    public partial class addedTypesMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "TypeAppeal",
                keyColumn: "ID",
                keyValue: 1,
                column: "TypeName",
                value: "Трудности в общении");

            migrationBuilder.UpdateData(
                table: "TypeAppeal",
                keyColumn: "ID",
                keyValue: 2,
                column: "TypeName",
                value: "Семейные проблемы");

            migrationBuilder.InsertData(
                table: "TypeAppeal",
                columns: new[] { "ID", "Description", "TypeName" },
                values: new object[,]
                {
                    { 3, "Описание типа 3", "Фобии" },
                    { 4, "Описание типа 4", "Депрессивные, кризисные состояния" },
                    { 5, "Описание типа 5", "Другое" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TypeAppeal",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "TypeAppeal",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "TypeAppeal",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "TypeAppeal",
                keyColumn: "ID",
                keyValue: 1,
                column: "TypeName",
                value: "Тип1");

            migrationBuilder.UpdateData(
                table: "TypeAppeal",
                keyColumn: "ID",
                keyValue: 2,
                column: "TypeName",
                value: "Тип2");
        }
    }
}
