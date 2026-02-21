using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizate.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserQuizAndQuizType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "quiz_types",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    display_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_quiz_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false),
                    email = table.Column<string>(type: "text", nullable: true),
                    email_verified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    hashed_password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "quizzes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    thumbnail_url = table.Column<string>(type: "text", nullable: true),
                    is_public = table.Column<bool>(type: "boolean", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    quiz_type_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_quizzes", x => x.id);
                    table.ForeignKey(
                        name: "fk_quizzes_quiz_types_quiz_type_id",
                        column: x => x.quiz_type_id,
                        principalTable: "quiz_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_quizzes_users_creator_id",
                        column: x => x.creator_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "quiz_types",
                columns: new[] { "id", "created_at", "display_name", "name", "updated_at" },
                values: new object[] { new Guid("12345678-1234-1234-1234-123456789012"), new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc).AddTicks(1654), "Standard", "standard", new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc).AddTicks(1654) });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "email", "email_verified_at", "hashed_password", "updated_at", "username" },
                values: new object[] { new Guid("655a37fa-b9e1-4cad-a684-383ac587e906"), new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc).AddTicks(1654), "demo_user@example.com", null, "hashed_password_placeholder", new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc).AddTicks(1654), "demo_user" });

            migrationBuilder.InsertData(
                table: "quizzes",
                columns: new[] { "id", "created_at", "creator_id", "description", "is_public", "quiz_type_id", "thumbnail_url", "title", "updated_at" },
                values: new object[] { new Guid("d1b2c3d4-e5f6-7890-abcd-ef1234567890"), new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc).AddTicks(1654), new Guid("655a37fa-b9e1-4cad-a684-383ac587e906"), "This is a sample quiz.", true, new Guid("12345678-1234-1234-1234-123456789012"), null, "Sample Quiz", new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc).AddTicks(1654) });

            migrationBuilder.CreateIndex(
                name: "ix_quizzes_creator_id",
                table: "quizzes",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "ix_quizzes_quiz_type_id",
                table: "quizzes",
                column: "quiz_type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "quizzes");

            migrationBuilder.DropTable(
                name: "quiz_types");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
