using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CinemaLibrary.Migrations
{
    public partial class firstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TypeAppeal",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypeName = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeAppeal", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NumberPhone = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Pass = table.Column<string>(type: "text", nullable: false),
                    IsPsychologist = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Appeal",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypeAppealID = table.Column<int>(type: "integer", nullable: false),
                    UserID = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appeal", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Appeal_TypeAppeal_TypeAppealID",
                        column: x => x.TypeAppealID,
                        principalTable: "TypeAppeal",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appeal_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Psychologist",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserID = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    MiddleName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Psychologist", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Psychologist_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Token = table.Column<string>(type: "text", nullable: false),
                    UserID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Token);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppealID = table.Column<int>(type: "integer", nullable: false),
                    AnswerText = table.Column<string>(type: "text", nullable: false),
                    PsychologistID = table.Column<int>(type: "integer", nullable: true),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Answer_Appeal_AppealID",
                        column: x => x.AppealID,
                        principalTable: "Appeal",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Answer_Psychologist_PsychologistID",
                        column: x => x.PsychologistID,
                        principalTable: "Psychologist",
                        principalColumn: "ID");
                });

            migrationBuilder.InsertData(
                table: "TypeAppeal",
                columns: new[] { "ID", "Description", "TypeName" },
                values: new object[,]
                {
                    { 1, "Описание типа 1", "Тип1" },
                    { 2, "Описание типа 2", "Тип2" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "ID", "Email", "IsPsychologist", "Name", "NumberPhone", "Pass" },
                values: new object[,]
                {
                    { 1, "psihologist1@mail.ru", true, "Сергей", "+79539449856", "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3" },
                    { 2, "psihologist2@mail.ru", true, "Василий", "+79226475612", "a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3" }
                });

            migrationBuilder.InsertData(
                table: "Psychologist",
                columns: new[] { "ID", "MiddleName", "Name", "Surname", "UserID" },
                values: new object[,]
                {
                    { 1, "Леонидович", "Сергей", "Кукушкин", 1 },
                    { 2, "Генадьевич", "Василий", "Безумов", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answer_AppealID",
                table: "Answer",
                column: "AppealID");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_PsychologistID",
                table: "Answer",
                column: "PsychologistID");

            migrationBuilder.CreateIndex(
                name: "IX_Appeal_TypeAppealID",
                table: "Appeal",
                column: "TypeAppealID");

            migrationBuilder.CreateIndex(
                name: "IX_Appeal_UserID",
                table: "Appeal",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Psychologist_UserID",
                table: "Psychologist",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserID",
                table: "RefreshTokens",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_User_NumberPhone",
                table: "User",
                column: "NumberPhone",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Appeal");

            migrationBuilder.DropTable(
                name: "Psychologist");

            migrationBuilder.DropTable(
                name: "TypeAppeal");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
