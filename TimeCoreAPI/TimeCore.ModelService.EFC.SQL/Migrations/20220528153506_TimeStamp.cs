using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeCore.ModelService.EFC.SQL.Migrations
{
    public partial class TimeStamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    AccountID = table.Column<int>(type: "int", nullable: true),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeStamp_AccountID",
                table: "TimeStamp",
                column: "AccountID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeStamp");
        }
    }
}
