using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dotnet9.WebAPI.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBlogPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppBlogPosts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Slug = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Cover = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Content = table.Column<string>(type: "text", maxLength: 2147483647, nullable: false),
                    CopyrightType = table.Column<int>(type: "integer", nullable: false),
                    Original = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    OriginalAvatar = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    OriginalTitle = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    OriginalLink = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Visible = table.Column<bool>(type: "boolean", nullable: false),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppBlogPosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppBlogPostAlbums",
                columns: table => new
                {
                    BlogPostId = table.Column<Guid>(type: "uuid", nullable: false),
                    AlbumId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppBlogPostAlbums", x => new { x.BlogPostId, x.AlbumId });
                    table.ForeignKey(
                        name: "FK_AppBlogPostAlbums_AppAlbums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "AppAlbums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppBlogPostAlbums_AppBlogPosts_BlogPostId",
                        column: x => x.BlogPostId,
                        principalTable: "AppBlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppBlogPostCategories",
                columns: table => new
                {
                    BlogPostId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppBlogPostCategories", x => new { x.BlogPostId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_AppBlogPostCategories_AppBlogPosts_BlogPostId",
                        column: x => x.BlogPostId,
                        principalTable: "AppBlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppBlogPostCategories_AppCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "AppCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppBlogPostTags",
                columns: table => new
                {
                    BlogPostId = table.Column<Guid>(type: "uuid", nullable: false),
                    TagId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppBlogPostTags", x => new { x.BlogPostId, x.TagId });
                    table.ForeignKey(
                        name: "FK_AppBlogPostTags_AppBlogPosts_BlogPostId",
                        column: x => x.BlogPostId,
                        principalTable: "AppBlogPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppBlogPostTags_AppTags_TagId",
                        column: x => x.TagId,
                        principalTable: "AppTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppBlogPostAlbums_AlbumId",
                table: "AppBlogPostAlbums",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_AppBlogPostAlbums_BlogPostId_AlbumId",
                table: "AppBlogPostAlbums",
                columns: new[] { "BlogPostId", "AlbumId" });

            migrationBuilder.CreateIndex(
                name: "IX_AppBlogPostCategories_BlogPostId_CategoryId",
                table: "AppBlogPostCategories",
                columns: new[] { "BlogPostId", "CategoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_AppBlogPostCategories_CategoryId",
                table: "AppBlogPostCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AppBlogPosts_Content",
                table: "AppBlogPosts",
                column: "Content");

            migrationBuilder.CreateIndex(
                name: "IX_AppBlogPosts_Description",
                table: "AppBlogPosts",
                column: "Description");

            migrationBuilder.CreateIndex(
                name: "IX_AppBlogPosts_Original",
                table: "AppBlogPosts",
                column: "Original");

            migrationBuilder.CreateIndex(
                name: "IX_AppBlogPosts_OriginalTitle",
                table: "AppBlogPosts",
                column: "OriginalTitle");

            migrationBuilder.CreateIndex(
                name: "IX_AppBlogPosts_Slug",
                table: "AppBlogPosts",
                column: "Slug");

            migrationBuilder.CreateIndex(
                name: "IX_AppBlogPosts_Title",
                table: "AppBlogPosts",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_AppBlogPostTags_BlogPostId_TagId",
                table: "AppBlogPostTags",
                columns: new[] { "BlogPostId", "TagId" });

            migrationBuilder.CreateIndex(
                name: "IX_AppBlogPostTags_TagId",
                table: "AppBlogPostTags",
                column: "TagId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppBlogPostAlbums");

            migrationBuilder.DropTable(
                name: "AppBlogPostCategories");

            migrationBuilder.DropTable(
                name: "AppBlogPostTags");

            migrationBuilder.DropTable(
                name: "AppBlogPosts");
        }
    }
}
