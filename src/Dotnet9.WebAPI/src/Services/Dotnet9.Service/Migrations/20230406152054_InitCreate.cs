using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dotnet9.Service.Migrations
{
    public partial class InitCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FriendlyLinks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Index = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Creator = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Modifier = table.Column<int>(type: "integer", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FriendlyLinks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IntegrationEventLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    EventTypeName = table.Column<string>(type: "text", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    TimesSent = table.Column<int>(type: "integer", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModificationTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    TransactionId = table.Column<Guid>(type: "uuid", nullable: false),
                    RowVersion = table.Column<string>(type: "character varying(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationEventLog", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventId_Version",
                table: "IntegrationEventLog",
                columns: new[] { "EventId", "RowVersion" });

            migrationBuilder.CreateIndex(
                name: "IX_State_MTime",
                table: "IntegrationEventLog",
                columns: new[] { "State", "ModificationTime" });

            migrationBuilder.CreateIndex(
                name: "IX_State_TimesSent_MTime",
                table: "IntegrationEventLog",
                columns: new[] { "State", "TimesSent", "ModificationTime" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FriendlyLinks");

            migrationBuilder.DropTable(
                name: "IntegrationEventLog");
        }
    }
}
