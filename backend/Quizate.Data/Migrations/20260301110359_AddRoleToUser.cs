using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Quizate.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "role",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: new Guid("655a37fa-b9e1-4cad-a684-383ac587e906"),
                column: "role",
                value: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "role",
                table: "users");
        }
    }
}
