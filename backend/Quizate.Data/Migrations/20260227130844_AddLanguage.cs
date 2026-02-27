using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizate.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLanguage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "language_code",
                table: "quizzes",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "languages",
                columns: table => new
                {
                    code = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_languages", x => x.code);
                });

            migrationBuilder.InsertData(
                table: "languages",
                columns: new[] { "code", "name" },
                values: new object[] { "en", "English" });

            migrationBuilder.UpdateData(
                table: "quizzes",
                keyColumn: "id",
                keyValue: new Guid("d1b2c3d4-e5f6-7890-abcd-ef1234567890"),
                column: "language_code",
                value: "en");

            migrationBuilder.CreateIndex(
                name: "ix_quizzes_language_code",
                table: "quizzes",
                column: "language_code");

            migrationBuilder.AddForeignKey(
                name: "fk_quizzes_languages_language_code",
                table: "quizzes",
                column: "language_code",
                principalTable: "languages",
                principalColumn: "code",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_quizzes_languages_language_code",
                table: "quizzes");

            migrationBuilder.DropTable(
                name: "languages");

            migrationBuilder.DropIndex(
                name: "ix_quizzes_language_code",
                table: "quizzes");

            migrationBuilder.DropColumn(
                name: "language_code",
                table: "quizzes");
        }
    }
}
