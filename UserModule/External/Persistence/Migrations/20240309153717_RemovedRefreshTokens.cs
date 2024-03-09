using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemovedRefreshTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_RefreshToken_RefreshTokenId",
                schema: "users",
                table: "Users");

            migrationBuilder.DropTable(
                name: "RefreshToken",
                schema: "users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RefreshTokenId",
                schema: "users",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RefreshTokenId",
                schema: "users",
                table: "Users");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RefreshTokenId",
                schema: "users",
                table: "Users",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "RefreshToken",
                schema: "users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    TokenDates_TokenCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TokenDates_TokenExpires = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_RefreshTokenId",
                schema: "users",
                table: "Users",
                column: "RefreshTokenId");

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
