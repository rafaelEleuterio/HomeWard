using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeWard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddJustification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Justification",
                table: "SessionTransitionDocuments",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Justification",
                table: "SessionTransitionDocuments");
        }
    }
}
