using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Revenue.API.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Revenue");

            migrationBuilder.CreateSequence(
                name: "TripSeq",
                schema: "Revenue",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "Trips",
                schema: "Revenue",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    BusId = table.Column<Guid>(nullable: false),
                    ConductorId = table.Column<Guid>(nullable: false),
                    DriverId = table.Column<Guid>(nullable: false),
                    TotalRevenue = table.Column<decimal>(nullable: false),
                    TotalTrips = table.Column<int>(nullable: false),
                    TripDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TripLegs",
                schema: "Revenue",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Revenue = table.Column<decimal>(nullable: false),
                    Route = table.Column<string>(nullable: true),
                    TripId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripLegs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripLegs_Trips_TripId",
                        column: x => x.TripId,
                        principalSchema: "Revenue",
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TripLegs_TripId",
                schema: "Revenue",
                table: "TripLegs",
                column: "TripId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TripLegs",
                schema: "Revenue");

            migrationBuilder.DropTable(
                name: "Trips",
                schema: "Revenue");

            migrationBuilder.DropSequence(
                name: "TripSeq",
                schema: "Revenue");
        }
    }
}
