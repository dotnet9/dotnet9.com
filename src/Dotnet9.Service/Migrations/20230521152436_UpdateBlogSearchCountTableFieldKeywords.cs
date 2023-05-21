using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dotnet9.Service.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBlogSearchCountTableFieldKeywords : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Slug",
                table: "BlogsSearchCounts",
                newName: "Keywords");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Keywords",
                table: "BlogsSearchCounts",
                newName: "Slug");
        }
    }
}
