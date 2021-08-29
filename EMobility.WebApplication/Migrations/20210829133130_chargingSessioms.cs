using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EMobility.WebApi.Migrations
{
    public partial class chargingSessioms : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChargePointId",
                table: "ChargingPoints",
                newName: "ChargingPointId");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChargingSessions");

            migrationBuilder.RenameColumn(
                name: "ChargingPointId",
                table: "ChargingPoints",
                newName: "ChargePointId");
        }
    }
}
