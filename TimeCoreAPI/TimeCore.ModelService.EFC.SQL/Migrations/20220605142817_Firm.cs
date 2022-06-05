using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeCore.ModelService.EFC.SQL.Migrations
{
    public partial class Firm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Firm",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<int>(type: "int", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firm", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Right",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RightID = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Right", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Workshop",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Number = table.Column<int>(type: "int", nullable: false),
                    FirmID = table.Column<int>(type: "int", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workshop", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Workshop_Firm_FirmID",
                        column: x => x.FirmID,
                        principalTable: "Firm",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GUID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkshopID = table.Column<int>(type: "int", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Account_Workshop_WorkshopID",
                        column: x => x.WorkshopID,
                        principalTable: "Workshop",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccountRight",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RightID = table.Column<int>(type: "int", nullable: false),
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountRight", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AccountRight_Account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeStamp",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeStampYear = table.Column<int>(type: "int", nullable: false),
                    TimeStampMonth = table.Column<int>(type: "int", nullable: false),
                    TimeStampDay = table.Column<int>(type: "int", nullable: false),
                    TimeStampHour = table.Column<int>(type: "int", nullable: false),
                    TimeStampMinute = table.Column<int>(type: "int", nullable: false),
                    TimeStampSecond = table.Column<int>(type: "int", nullable: false),
                    StampIn = table.Column<bool>(type: "bit", nullable: false),
                    AccountID = table.Column<int>(type: "int", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeStamp", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TimeStamp_Account_AccountID",
                        column: x => x.AccountID,
                        principalTable: "Account",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_WorkshopID",
                table: "Account",
                column: "WorkshopID");

            migrationBuilder.CreateIndex(
                name: "IX_AccountRight_AccountID",
                table: "AccountRight",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_TimeStamp_AccountID",
                table: "TimeStamp",
                column: "AccountID");

            migrationBuilder.CreateIndex(
                name: "IX_Workshop_FirmID",
                table: "Workshop",
                column: "FirmID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountRight");

            migrationBuilder.DropTable(
                name: "Right");

            migrationBuilder.DropTable(
                name: "TimeStamp");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Workshop");

            migrationBuilder.DropTable(
                name: "Firm");
        }
    }
}
