using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeWard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SaveFileToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "SessionTransitionDocuments");

            migrationBuilder.AddColumn<byte[]>(
                name: "Content",
                table: "SessionTransitionDocuments",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<long>(
                name: "FileSize",
                table: "SessionTransitionDocuments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "SessionTransitionDocuments");

            migrationBuilder.DropColumn(
                name: "FileSize",
                table: "SessionTransitionDocuments");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "SessionTransitionDocuments",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
