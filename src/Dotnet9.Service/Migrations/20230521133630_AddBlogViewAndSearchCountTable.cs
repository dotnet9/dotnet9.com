using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dotnet9.Service.Migrations
{
    /// <inheritdoc />
    public partial class AddBlogViewAndSearchCountTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ExpandContent",
                table: "IntegrationEventLog",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "BlogsSearchCounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false),
                    Ip = table.Column<string>(type: "text", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogsSearchCounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BlogsViewCounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Slug = table.Column<string>(type: "text", nullable: false),
                    Ip = table.Column<string>(type: "text", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogsViewCounts", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogsSearchCounts");

            migrationBuilder.DropTable(
                name: "BlogsViewCounts");

            migrationBuilder.DropColumn(
                name: "ExpandContent",
                table: "IntegrationEventLog");
        }
    }
}
