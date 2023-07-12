using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dotnet9.Service.Migrations
{
    /// <inheritdoc />
    public partial class AddLastModifyUserForBlog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastModifyUser",
                table: "Blogs",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastModifyUser",
                table: "Blogs");
        }
    }
}
