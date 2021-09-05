using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MasterReport.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataSourceTypes",
                columns: table => new
                {
                    DataSourceTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    DataSourceName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSourceTypes", x => x.DataSourceTypeId);
                });

            migrationBuilder.CreateTable(
                name: "ExecutionTypes",
                columns: table => new
                {
                    ExecutionTypeId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TypeName = table.Column<string>(type: "TEXT", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExecutionTypes", x => x.ExecutionTypeId);
                });

            migrationBuilder.CreateTable(
                name: "SslOptions",
                columns: table => new
                {
                    SslOptionsId = table.Column<int>(type: "INTEGER", nullable: false),
                    Option = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SslOptions", x => x.SslOptionsId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 60, nullable: false),
                    LastLogin = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "DataSources",
                columns: table => new
                {
                    DataSourceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    DataSourceTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    ConnectionString = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSources", x => x.DataSourceId);
                    table.ForeignKey(
                        name: "FK_DataSources_DataSourceTypes_DataSourceTypeId",
                        column: x => x.DataSourceTypeId,
                        principalTable: "DataSourceTypes",
                        principalColumn: "DataSourceTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailAccounts",
                columns: table => new
                {
                    EmailAccountId = table.Column<Guid>(type: "TEXT", nullable: false),
                    SmtpServer = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    User = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Port = table.Column<int>(type: "INTEGER", nullable: false),
                    SslOptionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailAccounts", x => x.EmailAccountId);
                    table.ForeignKey(
                        name: "FK_EmailAccounts_SslOptions_SslOptionId",
                        column: x => x.SslOptionId,
                        principalTable: "SslOptions",
                        principalColumn: "SslOptionsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    ReportId = table.Column<Guid>(type: "TEXT", nullable: false),
                    EmailAccountId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true),
                    LastExecution = table.Column<DateTime>(type: "TEXT", nullable: true),
                    NextExecution = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Active = table.Column<bool>(type: "INTEGER", nullable: false),
                    Force = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.ReportId);
                    table.ForeignKey(
                        name: "FK_Reports_EmailAccounts_EmailAccountId",
                        column: x => x.EmailAccountId,
                        principalTable: "EmailAccounts",
                        principalColumn: "EmailAccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Destinies",
                columns: table => new
                {
                    DestinyId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ReportId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Destinies", x => x.DestinyId);
                    table.ForeignKey(
                        name: "FK_Destinies_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "ReportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    DocumentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ReportId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Query = table.Column<string>(type: "TEXT", maxLength: 8000, nullable: false),
                    DataSourceId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.DocumentId);
                    table.ForeignKey(
                        name: "FK_Documents_DataSources_DataSourceId",
                        column: x => x.DataSourceId,
                        principalTable: "DataSources",
                        principalColumn: "DataSourceId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documents_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "ReportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExecutionHistories",
                columns: table => new
                {
                    ExecutionHistoryId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReportId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ExecutionTypeId = table.Column<int>(type: "INTEGER", nullable: false),
                    EventDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Obs = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    Succeeded = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExecutionHistories", x => x.ExecutionHistoryId);
                    table.ForeignKey(
                        name: "FK_ExecutionHistories_ExecutionTypes_ExecutionTypeId",
                        column: x => x.ExecutionTypeId,
                        principalTable: "ExecutionTypes",
                        principalColumn: "ExecutionTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExecutionHistories_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "ReportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DataSourceTypes",
                columns: new[] { "DataSourceTypeId", "DataSourceName" },
                values: new object[] { 1, "Microsoft SQL Server" });

            migrationBuilder.InsertData(
                table: "DataSourceTypes",
                columns: new[] { "DataSourceTypeId", "DataSourceName" },
                values: new object[] { 2, "MySQL" });

            migrationBuilder.InsertData(
                table: "DataSourceTypes",
                columns: new[] { "DataSourceTypeId", "DataSourceName" },
                values: new object[] { 3, "PostgreSQL" });

            migrationBuilder.InsertData(
                table: "ExecutionTypes",
                columns: new[] { "ExecutionTypeId", "TypeName" },
                values: new object[] { 1, "Manual" });

            migrationBuilder.InsertData(
                table: "ExecutionTypes",
                columns: new[] { "ExecutionTypeId", "TypeName" },
                values: new object[] { 2, "Scheduled" });

            migrationBuilder.InsertData(
                table: "SslOptions",
                columns: new[] { "SslOptionsId", "Option" },
                values: new object[] { 1, "None" });

            migrationBuilder.InsertData(
                table: "SslOptions",
                columns: new[] { "SslOptionsId", "Option" },
                values: new object[] { 2, "Auto" });

            migrationBuilder.InsertData(
                table: "SslOptions",
                columns: new[] { "SslOptionsId", "Option" },
                values: new object[] { 3, "SslOnConnect" });

            migrationBuilder.InsertData(
                table: "SslOptions",
                columns: new[] { "SslOptionsId", "Option" },
                values: new object[] { 4, "StartTls" });

            migrationBuilder.InsertData(
                table: "SslOptions",
                columns: new[] { "SslOptionsId", "Option" },
                values: new object[] { 5, "StartTlsWhenAvailable" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Active", "Email", "LastLogin", "Name", "Password" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), true, "admin@admin.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin", "$2a$14$4yrM/g9wcEbukfdtmvB4KOiZPLut5.VPX.MAsYUkNzCV/hyRB3DA2" });

            migrationBuilder.CreateIndex(
                name: "IX_DataSources_DataSourceTypeId",
                table: "DataSources",
                column: "DataSourceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Destinies_ReportId",
                table: "Destinies",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_DataSourceId",
                table: "Documents",
                column: "DataSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ReportId",
                table: "Documents",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailAccounts_SslOptionId",
                table: "EmailAccounts",
                column: "SslOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExecutionHistories_ExecutionTypeId",
                table: "ExecutionHistories",
                column: "ExecutionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ExecutionHistories_ReportId",
                table: "ExecutionHistories",
                column: "ReportId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_EmailAccountId",
                table: "Reports",
                column: "EmailAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Destinies");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "ExecutionHistories");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "DataSources");

            migrationBuilder.DropTable(
                name: "ExecutionTypes");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "DataSourceTypes");

            migrationBuilder.DropTable(
                name: "EmailAccounts");

            migrationBuilder.DropTable(
                name: "SslOptions");
        }
    }
}
