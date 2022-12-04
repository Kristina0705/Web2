using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaLibrary.Migrations
{
    public partial class M_ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "ID", "Email", "IsPsychologist", "Name", "Pass", "Surname" },
                values: new object[] { 3, "auxilium_psixo@mail.ru", true, "Иван", "6b86b273ff34fce19d6b804eff5a3f5747ada4eaa22f1d49c01e52ddb7875b4b", "Иванов" });

            migrationBuilder.InsertData(
                table: "Psychologist",
                columns: new[] { "ID", "MiddleName", "Name", "Surname", "UserID" },
                values: new object[] { 3, "Иванович", "Иван", "Иванов", 3 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Psychologist",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "ID",
                keyValue: 3);
        }
    }
}
