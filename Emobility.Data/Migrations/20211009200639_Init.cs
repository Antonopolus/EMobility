using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EMobility.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChargingPoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RestUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChargingPointId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargingPoints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChargingSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SessionId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LocalStartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    DurationOf = table.Column<TimeSpan>(type: "time", nullable: false),
                    Energy = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RfidTag = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChargingStation = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChargingSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConnectionStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChargingPointId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionStates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RfidTagOwner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Owner = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RfidTagId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    From = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Until = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RfidTagOwner", x => x.Id);
                    table.CheckConstraint("UntilAfterFrom", "[Until] IS NULL OR [Until] > [From]");
                });

            migrationBuilder.InsertData(
                table: "ChargingPoints",
                columns: new[] { "Id", "ChargingPointId", "Name", "RestUrl" },
                values: new object[,]
                {
                    { -4, "CP -4", "", "" },
                    { -5, "CP -5", "", "" },
                    { 1, "CP 1", "", "" },
                    { 2, "CP 2", "", "" },
                    { 3, "CP 3", "", "" },
                    { 4, "CP 4", "", "" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChargingPoints_ChargingPointId",
                table: "ChargingPoints",
                column: "ChargingPointId",
                unique: true,
                filter: "[ChargingPointId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RfidTagOwner_RfidTagId",
                table: "RfidTagOwner",
                column: "RfidTagId",
                unique: true,
                filter: "[RfidTagId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChargingPoints");

            migrationBuilder.DropTable(
                name: "ChargingSessions");

            migrationBuilder.DropTable(
                name: "ConnectionStates");

            migrationBuilder.DropTable(
                name: "RfidTagOwner");
        }
    }
}
