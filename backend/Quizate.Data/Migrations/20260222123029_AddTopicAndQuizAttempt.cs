using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Quizate.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTopicAndQuizAttempt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "quiz_attempts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quiz_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    score = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_quiz_attempts", x => x.id);
                    table.ForeignKey(
                        name: "fk_quiz_attempts_quizzes_quiz_id",
                        column: x => x.quiz_id,
                        principalTable: "quizzes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_quiz_attempts_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "topics",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    display_name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_topics", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "quiz_topic",
                columns: table => new
                {
                    quizzes_id = table.Column<Guid>(type: "uuid", nullable: false),
                    topics_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_quiz_topic", x => new { x.quizzes_id, x.topics_id });
                    table.ForeignKey(
                        name: "fk_quiz_topic_quizzes_quizzes_id",
                        column: x => x.quizzes_id,
                        principalTable: "quizzes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_quiz_topic_topics_topics_id",
                        column: x => x.topics_id,
                        principalTable: "topics",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "quiz_attempts",
                columns: new[] { "id", "created_at", "quiz_id", "score", "user_id" },
                values: new object[] { 1, new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc), new Guid("d1b2c3d4-e5f6-7890-abcd-ef1234567890"), 50, new Guid("655a37fa-b9e1-4cad-a684-383ac587e906") });

            migrationBuilder.InsertData(
                table: "topics",
                columns: new[] { "id", "created_at", "description", "display_name", "name", "updated_at" },
                values: new object[] { new Guid("abcdef12-3456-7890-abcd-ef1234567890"), new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc).AddTicks(1654), null, "Geography", "geography", new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc).AddTicks(1654) });

            migrationBuilder.CreateIndex(
                name: "ix_quiz_attempts_quiz_id",
                table: "quiz_attempts",
                column: "quiz_id");

            migrationBuilder.CreateIndex(
                name: "ix_quiz_attempts_user_id",
                table: "quiz_attempts",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_quiz_topic_topics_id",
                table: "quiz_topic",
                column: "topics_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "quiz_attempts");

            migrationBuilder.DropTable(
                name: "quiz_topic");

            migrationBuilder.DropTable(
                name: "topics");
        }
    }
}
