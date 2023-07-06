using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dotnet9.Service.Migrations
{
    /// <inheritdoc />
    public partial class AddIsEmptyToBlogSearchCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEmpty",
                table: "BlogsSearchCounts",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEmpty",
                table: "BlogsSearchCounts");
        }
    }
}
