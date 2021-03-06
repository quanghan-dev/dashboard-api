using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Persistence.Migrations.Postgre
{
    public partial class PostgreSQL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "Accounts",
                schema: "public",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
                    Fullname = table.Column<string>(type: "text", nullable: true),
                    ActivateCode = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Department = table.Column<string>(type: "text", nullable: true),
                    Project = table.Column<string>(type: "text", nullable: true),
                    Avatar = table.Column<string>(type: "text", nullable: true),
                    EmployeeId = table.Column<Guid>(type: "uuid", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Accounts_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "public",
                        principalTable: "Accounts",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Dashboards",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    LayoutType = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dashboards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dashboards_Accounts_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "Accounts",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskName = table.Column<string>(type: "text", nullable: true),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Accounts_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "Accounts",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                schema: "public",
                columns: table => new
                {
                    RefreshToken = table.Column<Guid>(type: "uuid", nullable: false),
                    IsRevoked = table.Column<bool>(type: "boolean", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ExpiredDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.RefreshToken);
                    table.ForeignKey(
                        name: "FK_Tokens_Accounts_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "Accounts",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Widgets",
                schema: "public",
                columns: table => new
                {
                    WidgetId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    WidgetType = table.Column<string>(type: "text", nullable: true),
                    MinWidth = table.Column<int>(type: "integer", nullable: true),
                    minHeight = table.Column<int>(type: "integer", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    DashboardId = table.Column<Guid>(type: "uuid", nullable: false),
                    Configs = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Widgets", x => x.WidgetId);
                    table.ForeignKey(
                        name: "FK_Widgets_Dashboards_DashboardId",
                        column: x => x.DashboardId,
                        principalSchema: "public",
                        principalTable: "Dashboards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_EmployeeId",
                schema: "public",
                table: "Contacts",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Dashboards_UserId",
                schema: "public",
                table: "Dashboards",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_UserId",
                schema: "public",
                table: "Tasks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_UserId",
                schema: "public",
                table: "Tokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Widgets_DashboardId",
                schema: "public",
                table: "Widgets",
                column: "DashboardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Tasks",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Tokens",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Widgets",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Dashboards",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Accounts",
                schema: "public");
        }
    }
}
