using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Quizate.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddQuestionAndOption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "multiple_choice_questions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    quiz_id = table.Column<Guid>(type: "uuid", nullable: false),
                    text = table.Column<string>(type: "text", nullable: false),
                    image_url = table.Column<string>(type: "text", nullable: true),
                    display_order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_multiple_choice_questions", x => x.id);
                    table.ForeignKey(
                        name: "fk_multiple_choice_questions_quizzes_quiz_id",
                        column: x => x.quiz_id,
                        principalTable: "quizzes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "multiple_choice_options",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    question_id = table.Column<int>(type: "integer", nullable: false),
                    text = table.Column<string>(type: "text", nullable: false),
                    is_correct = table.Column<bool>(type: "boolean", nullable: false),
                    display_order = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_multiple_choice_options", x => x.id);
                    table.ForeignKey(
                        name: "fk_multiple_choice_options_multiple_choice_questions_question_",
                        column: x => x.question_id,
                        principalTable: "multiple_choice_questions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "multiple_choice_questions",
                columns: new[] { "id", "display_order", "image_url", "quiz_id", "text" },
                values: new object[] { 1, 1, null, new Guid("d1b2c3d4-e5f6-7890-abcd-ef1234567890"), "What is the capital of France?" });

            migrationBuilder.InsertData(
                table: "multiple_choice_options",
                columns: new[] { "id", "display_order", "is_correct", "question_id", "text" },
                values: new object[,]
                {
                    { 1, 1, true, 1, "Paris" },
                    { 2, 2, false, 1, "London" },
                    { 3, 3, false, 1, "Rome" },
                    { 4, 4, false, 1, "Berlin" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_multiple_choice_options_question_id",
                table: "multiple_choice_options",
                column: "question_id");

            migrationBuilder.CreateIndex(
                name: "ix_multiple_choice_questions_quiz_id",
                table: "multiple_choice_questions",
                column: "quiz_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "multiple_choice_options");

            migrationBuilder.DropTable(
                name: "multiple_choice_questions");
        }
    }
}
