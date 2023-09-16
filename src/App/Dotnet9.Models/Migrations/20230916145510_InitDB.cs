using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Dotnet9.Models.Migrations
{
    /// <inheritdoc />
    public partial class InitDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()"),
                    UserName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Pwd = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    LockedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    LoginFailCount = table.Column<int>(type: "integer", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FriendLinks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    IsPublish = table.Column<bool>(type: "boolean", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendLinks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostCates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CateName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostCates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Thumb = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Snippet = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ReadCount = table.Column<int>(type: "integer", nullable: false),
                    IsPublish = table.Column<bool>(type: "boolean", nullable: false),
                    IsTop = table.Column<bool>(type: "boolean", nullable: false),
                    IsDelete = table.Column<bool>(type: "boolean", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PostTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TagName = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysConfig",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Value = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    GroupName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AccountLoginRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    UA = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Ip = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    IsLogin = table.Column<bool>(type: "boolean", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountLoginRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountLoginRecord_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountRoleRelation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                    AccountRoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRoleRelation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccountRoleRelation_AccountRoles_AccountRoleId",
                        column: x => x.AccountRoleId,
                        principalTable: "AccountRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccountRoleRelation_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SysResources",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AccountId = table.Column<Guid>(type: "uuid", nullable: true),
                    Path = table.Column<string>(type: "text", nullable: false),
                    Suffix = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    ClientIp = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Size = table.Column<long>(type: "bigint", nullable: false),
                    StorageType = table.Column<int>(type: "integer", nullable: false),
                    StorageDomain = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SysResources_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PostCateRelations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PostCateId = table.Column<Guid>(type: "uuid", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostCateRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostCateRelations_PostCates_PostCateId",
                        column: x => x.PostCateId,
                        principalTable: "PostCates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostCateRelations_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PostComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    NickName = table.Column<string>(type: "text", nullable: false),
                    Pid = table.Column<int>(type: "integer", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostComments_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostVisitRecord",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    IP = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UA = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    UId = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostVisitRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostVisitRecord_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostTagRelations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PostId = table.Column<Guid>(type: "uuid", nullable: false),
                    PostTagsId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTagRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostTagRelations_PostTags_PostTagsId",
                        column: x => x.PostTagsId,
                        principalTable: "PostTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTagRelations_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Email", "LockedTime", "LoginFailCount", "Pwd", "UpdateTime", "UserName" },
                values: new object[] { new Guid("f5a726be-12f3-47b0-9245-d4b77d41ea4f"), "qq1012434131@gmail.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "JAvlGPq9JyTdtvBO6x2llnRI1+gxwIyPqCKAn3THIKk=", null, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_AccountLoginRecord_AccountId",
                table: "AccountLoginRecord",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountRoleRelation_AccountId",
                table: "AccountRoleRelation",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountRoleRelation_AccountRoleId",
                table: "AccountRoleRelation",
                column: "AccountRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_PostCateRelations_PostCateId",
                table: "PostCateRelations",
                column: "PostCateId");

            migrationBuilder.CreateIndex(
                name: "IX_PostCateRelations_PostId",
                table: "PostCateRelations",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostComments_PostId",
                table: "PostComments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTagRelations_PostId",
                table: "PostTagRelations",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostTagRelations_PostTagsId",
                table: "PostTagRelations",
                column: "PostTagsId");

            migrationBuilder.CreateIndex(
                name: "IX_PostVisitRecord_PostId",
                table: "PostVisitRecord",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_SysConfig_Key",
                table: "SysConfig",
                column: "Key",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SysResources_AccountId",
                table: "SysResources",
                column: "AccountId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountLoginRecord");

            migrationBuilder.DropTable(
                name: "AccountRoleRelation");

            migrationBuilder.DropTable(
                name: "FriendLinks");

            migrationBuilder.DropTable(
                name: "PostCateRelations");

            migrationBuilder.DropTable(
                name: "PostComments");

            migrationBuilder.DropTable(
                name: "PostTagRelations");

            migrationBuilder.DropTable(
                name: "PostVisitRecord");

            migrationBuilder.DropTable(
                name: "SysConfig");

            migrationBuilder.DropTable(
                name: "SysResources");

            migrationBuilder.DropTable(
                name: "AccountRoles");

            migrationBuilder.DropTable(
                name: "PostCates");

            migrationBuilder.DropTable(
                name: "PostTags");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}
