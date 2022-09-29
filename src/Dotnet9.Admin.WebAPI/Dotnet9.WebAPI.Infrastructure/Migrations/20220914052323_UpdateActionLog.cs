using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dotnet9.WebAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateActionLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UA",
                table: "AppActionLogs",
                newName: "Ua");

            migrationBuilder.RenameColumn(
                name: "OS",
                table: "AppActionLogs",
                newName: "Os");

            migrationBuilder.RenameColumn(
                name: "IP",
                table: "AppActionLogs",
                newName: "Ip");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Ua",
                table: "AppActionLogs",
                newName: "UA");

            migrationBuilder.RenameColumn(
                name: "Os",
                table: "AppActionLogs",
                newName: "OS");

            migrationBuilder.RenameColumn(
                name: "Ip",
                table: "AppActionLogs",
                newName: "IP");
        }
    }
}
