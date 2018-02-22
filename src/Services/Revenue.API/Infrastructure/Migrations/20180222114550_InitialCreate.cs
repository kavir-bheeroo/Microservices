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
                name: "Trip",
                schema: "Revenue",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    BusId = table.Column<Guid>(nullable: false),
                    ConductorId = table.Column<Guid>(nullable: false),
                    DriverId = table.Column<Guid>(nullable: false),
                    TripLegs_Capacity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trip", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trip",
                schema: "Revenue");

            migrationBuilder.DropSequence(
                name: "TripSeq",
                schema: "Revenue");
        }
    }
}
