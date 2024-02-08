using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class MinorFixesAgain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_RefreshToken_RefreshTokenId",
                schema: "users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshToken",
                schema: "users",
                table: "RefreshToken");

            migrationBuilder.RenameTable(
                name: "RefreshToken",
                schema: "users",
                newName: "RefreshTokens",
                newSchema: "users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshTokens",
                schema: "users",
                table: "RefreshTokens",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_RefreshTokens_RefreshTokenId",
                schema: "users",
                table: "Users",
                column: "RefreshTokenId",
                principalSchema: "users",
                principalTable: "RefreshTokens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_RefreshTokens_RefreshTokenId",
                schema: "users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshTokens",
                schema: "users",
                table: "RefreshTokens");

            migrationBuilder.RenameTable(
                name: "RefreshTokens",
                schema: "users",
                newName: "RefreshToken",
                newSchema: "users");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshToken",
                schema: "users",
                table: "RefreshToken",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_RefreshToken_RefreshTokenId",
                schema: "users",
                table: "Users",
                column: "RefreshTokenId",
                principalSchema: "users",
                principalTable: "RefreshToken",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
