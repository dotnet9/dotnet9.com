using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dotnet9.WebAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddViewCountToBlogPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "AppBlogPosts",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "AppBlogPosts");
        }
    }
}
