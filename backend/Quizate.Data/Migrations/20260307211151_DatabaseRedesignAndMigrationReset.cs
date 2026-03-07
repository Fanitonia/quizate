using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Quizate.Data.Migrations
{
    /// <inheritdoc />
    public partial class DatabaseRedesignAndMigrationReset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "question_types",
                columns: table => new
                {
                    name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_question_types", x => x.name);
                    table.CheckConstraint("ck_quiz_types_name_not_empty", "char_length(trim(name)) > 0");
                });

            migrationBuilder.CreateTable(
                name: "quiz_languages",
                columns: table => new
                {
                    code = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_quiz_languages", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "quiz_topics",
                columns: table => new
                {
                    name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    display_name = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_quiz_topics", x => x.name);
                    table.CheckConstraint("ck_topics_display_name_not_empty", "char_length(trim(display_name)) > 0");
                    table.CheckConstraint("ck_topics_name_not_empty", "char_length(trim(name)) > 0");
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    username = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false),
                    normalized_username = table.Column<string>(type: "character varying(25)", maxLength: 25, nullable: false, computedColumnSql: "lower(username)", stored: true),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    email_verified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    password_hash = table.Column<string>(type: "text", nullable: false),
                    role = table.Column<int>(type: "integer", nullable: false),
                    profile_picture_url = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                    table.CheckConstraint("ck_users_password_hash_not_empty", "char_length(trim(password_hash)) > 0");
                    table.CheckConstraint("ck_users_username_format", "username ~ '^[A-Za-z0-9_]+$'");
                    table.CheckConstraint("ck_users_username_not_empty", "char_length(trim(username)) > 0");
                });

            migrationBuilder.CreateTable(
                name: "quizzes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    creator_id = table.Column<Guid>(type: "uuid", nullable: true),
                    language_code = table.Column<string>(type: "character varying(10)", nullable: false),
                    title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true),
                    thumbnail_url = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    is_public = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_quizzes", x => x.id);
                    table.CheckConstraint("ck_quizzes_title_not_empty", "char_length(trim(title)) > 0");
                    table.ForeignKey(
                        name: "fk_quizzes_quiz_languages_language_code",
                        column: x => x.language_code,
                        principalTable: "quiz_languages",
                        principalColumn: "code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_quizzes_users_creator_id",
                        column: x => x.creator_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "refresh_tokens",
                columns: table => new
                {
                    token_hash = table.Column<string>(type: "text", nullable: false),
                    expires_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_refresh_tokens", x => x.token_hash);
                    table.CheckConstraint("ck_refresh_tokens_token_hash_not_empty", "char_length(trim(token_hash)) > 0");
                    table.ForeignKey(
                        name: "fk_refresh_tokens_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "questions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    quiz_id = table.Column<Guid>(type: "uuid", nullable: false),
                    question_type_name = table.Column<string>(type: "character varying(20)", nullable: false),
                    payload = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_questions", x => x.id);
                    table.ForeignKey(
                        name: "fk_questions_question_types_question_type_name",
                        column: x => x.question_type_name,
                        principalTable: "question_types",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_questions_quizzes_quiz_id",
                        column: x => x.quiz_id,
                        principalTable: "quizzes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "quiz_attempts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    quiz_id = table.Column<Guid>(type: "uuid", nullable: false),
                    score = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_quiz_attempts", x => x.id);
                    table.CheckConstraint("ck_quiz_attempts_score_non_negative", "score >= 0");
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
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "quiz_quiz_topic",
                columns: table => new
                {
                    quizzes_id = table.Column<Guid>(type: "uuid", nullable: false),
                    topics_name = table.Column<string>(type: "character varying(25)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_quiz_quiz_topic", x => new { x.quizzes_id, x.topics_name });
                    table.ForeignKey(
                        name: "fk_quiz_quiz_topic_quiz_topics_topics_name",
                        column: x => x.topics_name,
                        principalTable: "quiz_topics",
                        principalColumn: "name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_quiz_quiz_topic_quizzes_quizzes_id",
                        column: x => x.quizzes_id,
                        principalTable: "quizzes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "question_types",
                columns: new[] { "name", "created_at", "updated_at" },
                values: new object[] { "multiple_choice", new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc), new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "quiz_languages",
                column: "code",
                value: "en");

            migrationBuilder.InsertData(
                table: "quiz_topics",
                columns: new[] { "name", "created_at", "description", "display_name", "updated_at" },
                values: new object[] { "geography", new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc), null, "Geography", new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id", "created_at", "email", "email_verified_at", "is_deleted", "password_hash", "profile_picture_url", "role", "updated_at", "username" },
                values: new object[] { new Guid("655a37fa-b9e1-4cad-a684-383ac587e906"), new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc), "demo_user@example.com", null, false, "hashed_password_placeholder", null, 0, new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc), "demo_user" });

            migrationBuilder.InsertData(
                table: "quizzes",
                columns: new[] { "id", "created_at", "creator_id", "description", "is_public", "language_code", "thumbnail_url", "title", "updated_at" },
                values: new object[] { new Guid("d1b2c3d4-e5f6-7890-abcd-ef1234567890"), new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc), new Guid("655a37fa-b9e1-4cad-a684-383ac587e906"), "This is a sample quiz.", true, "en", null, "Sample Quiz", new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc) });

            migrationBuilder.InsertData(
                table: "questions",
                columns: new[] { "id", "payload", "question_type_name", "quiz_id" },
                values: new object[] { 1, "{\"Title\":\"What's the capital of Turkey?\",\"Options\":[{\"Text\":\"Istanbul\",\"IsCorrect\":false},{\"Text\":\"Ankara\",\"IsCorrect\":true},{\"Text\":\"Izmir\",\"IsCorrect\":false},{\"Text\":\"Bursa\",\"IsCorrect\":false}]}", "multiple_choice", new Guid("d1b2c3d4-e5f6-7890-abcd-ef1234567890") });

            migrationBuilder.InsertData(
                table: "quiz_attempts",
                columns: new[] { "id", "created_at", "quiz_id", "score", "user_id" },
                values: new object[] { 1, new DateTime(2026, 2, 20, 19, 17, 31, 60, DateTimeKind.Utc), new Guid("d1b2c3d4-e5f6-7890-abcd-ef1234567890"), 50, new Guid("655a37fa-b9e1-4cad-a684-383ac587e906") });

            migrationBuilder.CreateIndex(
                name: "ix_questions_question_type_name",
                table: "questions",
                column: "question_type_name");

            migrationBuilder.CreateIndex(
                name: "ix_questions_quiz_id",
                table: "questions",
                column: "quiz_id");

            migrationBuilder.CreateIndex(
                name: "ix_quiz_attempts_quiz_id",
                table: "quiz_attempts",
                column: "quiz_id");

            migrationBuilder.CreateIndex(
                name: "ix_quiz_attempts_user_id",
                table: "quiz_attempts",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_quiz_quiz_topic_topics_name",
                table: "quiz_quiz_topic",
                column: "topics_name");

            migrationBuilder.CreateIndex(
                name: "ix_quizzes_creator_id",
                table: "quizzes",
                column: "creator_id");

            migrationBuilder.CreateIndex(
                name: "ix_quizzes_language_code",
                table: "quizzes",
                column: "language_code");

            migrationBuilder.CreateIndex(
                name: "ix_refresh_tokens_user_id",
                table: "refresh_tokens",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_normalized_username",
                table: "users",
                column: "normalized_username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_username",
                table: "users",
                column: "username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "questions");

            migrationBuilder.DropTable(
                name: "quiz_attempts");

            migrationBuilder.DropTable(
                name: "quiz_quiz_topic");

            migrationBuilder.DropTable(
                name: "refresh_tokens");

            migrationBuilder.DropTable(
                name: "question_types");

            migrationBuilder.DropTable(
                name: "quiz_topics");

            migrationBuilder.DropTable(
                name: "quizzes");

            migrationBuilder.DropTable(
                name: "quiz_languages");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
