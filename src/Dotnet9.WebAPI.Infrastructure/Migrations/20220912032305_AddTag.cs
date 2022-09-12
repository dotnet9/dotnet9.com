using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dotnet9.WebAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTag : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppTags", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppTags_Name",
                table: "AppTags",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppTags");
        }
    }
}
