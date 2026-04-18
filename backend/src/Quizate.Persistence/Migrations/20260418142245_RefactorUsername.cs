using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizate.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RefactorUsername : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_users_normalized_username",
                table: "users");

            migrationBuilder.DropCheckConstraint(
                name: "ck_users_username_format",
                table: "users");

            migrationBuilder.DropColumn(
                name: "normalized_username",
                table: "users");

            migrationBuilder.AddColumn<string>(
                name: "display_name",
                table: "users",
                type: "character varying(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("655a37fa-b9e1-4cad-a684-383ac587e906"),
                column: "display_name",
                value: "demo_user");

            migrationBuilder.AddCheckConstraint(
                name: "ck_users_displayname_format",
                table: "users",
                sql: "display_name ~ '^[a-zA-Z0-9_ ]+$'");

            migrationBuilder.AddCheckConstraint(
                name: "ck_users_username_format",
                table: "users",
                sql: "username ~ '^[a-z0-9_]+$'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "ck_users_displayname_format",
                table: "users");

            migrationBuilder.DropCheckConstraint(
                name: "ck_users_username_format",
                table: "users");

            migrationBuilder.DropColumn(
                name: "display_name",
                table: "users");

            migrationBuilder.AddColumn<string>(
                name: "normalized_username",
                table: "users",
                type: "character varying(25)",
                maxLength: 25,
                nullable: false,
                computedColumnSql: "lower(username)",
                stored: true);

            migrationBuilder.CreateIndex(
                name: "ix_users_normalized_username",
                table: "users",
                column: "normalized_username",
                unique: true);

            migrationBuilder.AddCheckConstraint(
                name: "ck_users_username_format",
                table: "users",
                sql: "username ~ '^[A-Za-z0-9_]+$'");
        }
    }
}
