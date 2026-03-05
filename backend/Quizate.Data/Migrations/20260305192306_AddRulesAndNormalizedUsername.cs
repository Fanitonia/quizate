using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizate.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRulesAndNormalizedUsername : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "users",
                type: "character varying(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "users",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "topics",
                type: "character varying(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "display_name",
                table: "topics",
                type: "character varying(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "quizzes",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "thumbnail_url",
                table: "quizzes",
                type: "character varying(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "language_code",
                table: "quizzes",
                type: "character varying(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "quizzes",
                type: "character varying(400)",
                maxLength: 400,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "quiz_types",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "display_name",
                table: "quiz_types",
                type: "character varying(25)",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "text",
                table: "multiple_choice_questions",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "image_url",
                table: "multiple_choice_questions",
                type: "character varying(1024)",
                maxLength: 1024,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "text",
                table: "multiple_choice_options",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "languages",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "languages",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "normalized_username",
                table: "users",
                type: "character varying(25)",
                maxLength: 25,
                nullable: false,
                computedColumnSql: "lower(username)",
                stored: true);

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

            migrationBuilder.AddCheckConstraint(
                name: "ck_users_password_hash_not_empty",
                table: "users",
                sql: "char_length(trim(password_hash)) > 0");

            migrationBuilder.AddCheckConstraint(
                name: "ck_users_username_format",
                table: "users",
                sql: "username ~ '^[A-Za-z0-9_]+$'");

            migrationBuilder.AddCheckConstraint(
                name: "ck_users_username_not_empty",
                table: "users",
                sql: "char_length(trim(username)) > 0");

            migrationBuilder.CreateIndex(
                name: "ix_topics_name",
                table: "topics",
                column: "name",
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "ck_topics_display_name_not_empty",
                table: "topics",
                sql: "char_length(trim(display_name)) > 0");

            migrationBuilder.AddCheckConstraint(
                name: "ck_topics_name_not_empty",
                table: "topics",
                sql: "char_length(trim(name)) > 0");

            migrationBuilder.CreateIndex(
                name: "ix_refresh_tokens_token_hash",
                table: "refresh_tokens",
                column: "token_hash",
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "ck_refresh_tokens_token_hash_not_empty",
                table: "refresh_tokens",
                sql: "char_length(trim(token_hash)) > 0");

            migrationBuilder.AddCheckConstraint(
                name: "ck_quizzes_title_not_empty",
                table: "quizzes",
                sql: "char_length(trim(title)) > 0");

            migrationBuilder.CreateIndex(
                name: "ix_quiz_types_name",
                table: "quiz_types",
                column: "name",
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "ck_quiz_types_display_name_not_empty",
                table: "quiz_types",
                sql: "char_length(trim(display_name)) > 0");

            migrationBuilder.AddCheckConstraint(
                name: "ck_quiz_types_name_not_empty",
                table: "quiz_types",
                sql: "char_length(trim(name)) > 0");

            migrationBuilder.AddCheckConstraint(
                name: "ck_quiz_attempts_score_non_negative",
                table: "quiz_attempts",
                sql: "score >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "ck_multiple_choice_questions_display_order_non_negative",
                table: "multiple_choice_questions",
                sql: "display_order >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "ck_multiple_choice_questions_text_not_empty",
                table: "multiple_choice_questions",
                sql: "char_length(trim(text)) > 0");

            migrationBuilder.AddCheckConstraint(
                name: "ck_multiple_choice_options_display_order_non_negative",
                table: "multiple_choice_options",
                sql: "display_order >= 0");

            migrationBuilder.AddCheckConstraint(
                name: "ck_multiple_choice_options_text_not_empty",
                table: "multiple_choice_options",
                sql: "char_length(trim(text)) > 0");

            migrationBuilder.CreateIndex(
                name: "ix_languages_code",
                table: "languages",
                column: "code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_users_email",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_users_normalized_username",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_users_username",
                table: "users");

            migrationBuilder.DropCheckConstraint(
                name: "ck_users_password_hash_not_empty",
                table: "users");

            migrationBuilder.DropCheckConstraint(
                name: "ck_users_username_format",
                table: "users");

            migrationBuilder.DropCheckConstraint(
                name: "ck_users_username_not_empty",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ix_topics_name",
                table: "topics");

            migrationBuilder.DropCheckConstraint(
                name: "ck_topics_display_name_not_empty",
                table: "topics");

            migrationBuilder.DropCheckConstraint(
                name: "ck_topics_name_not_empty",
                table: "topics");

            migrationBuilder.DropIndex(
                name: "ix_refresh_tokens_token_hash",
                table: "refresh_tokens");

            migrationBuilder.DropCheckConstraint(
                name: "ck_refresh_tokens_token_hash_not_empty",
                table: "refresh_tokens");

            migrationBuilder.DropCheckConstraint(
                name: "ck_quizzes_title_not_empty",
                table: "quizzes");

            migrationBuilder.DropIndex(
                name: "ix_quiz_types_name",
                table: "quiz_types");

            migrationBuilder.DropCheckConstraint(
                name: "ck_quiz_types_display_name_not_empty",
                table: "quiz_types");

            migrationBuilder.DropCheckConstraint(
                name: "ck_quiz_types_name_not_empty",
                table: "quiz_types");

            migrationBuilder.DropCheckConstraint(
                name: "ck_quiz_attempts_score_non_negative",
                table: "quiz_attempts");

            migrationBuilder.DropCheckConstraint(
                name: "ck_multiple_choice_questions_display_order_non_negative",
                table: "multiple_choice_questions");

            migrationBuilder.DropCheckConstraint(
                name: "ck_multiple_choice_questions_text_not_empty",
                table: "multiple_choice_questions");

            migrationBuilder.DropCheckConstraint(
                name: "ck_multiple_choice_options_display_order_non_negative",
                table: "multiple_choice_options");

            migrationBuilder.DropCheckConstraint(
                name: "ck_multiple_choice_options_text_not_empty",
                table: "multiple_choice_options");

            migrationBuilder.DropIndex(
                name: "ix_languages_code",
                table: "languages");

            migrationBuilder.DropColumn(
                name: "normalized_username",
                table: "users");

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "users",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(25)",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "email",
                table: "users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "topics",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(25)",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "display_name",
                table: "topics",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(25)",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "title",
                table: "quizzes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "thumbnail_url",
                table: "quizzes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "language_code",
                table: "quizzes",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(10)");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "quizzes",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(400)",
                oldMaxLength: 400,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "quiz_types",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "display_name",
                table: "quiz_types",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(25)",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "text",
                table: "multiple_choice_questions",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AlterColumn<string>(
                name: "image_url",
                table: "multiple_choice_questions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1024)",
                oldMaxLength: 1024,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "text",
                table: "multiple_choice_options",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "languages",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "code",
                table: "languages",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(10)",
                oldMaxLength: 10);
        }
    }
}
