using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HomeWard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSessionTrasitionDocuments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SessionTransitionDocuments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SessionTransitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    ContentType = table.Column<string>(type: "text", nullable: false),
                    FilePath = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionTransitionDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionTransitionDocuments_SessionTransitions_SessionTransi~",
                        column: x => x.SessionTransitionId,
                        principalTable: "SessionTransitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SessionTransitionDocuments_SessionTransitionId",
                table: "SessionTransitionDocuments",
                column: "SessionTransitionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SessionTransitionDocuments");
        }
    }
}
