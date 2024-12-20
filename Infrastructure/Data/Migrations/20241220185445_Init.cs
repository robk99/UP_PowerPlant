using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PowerPlants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstalledPower = table.Column<float>(type: "real", nullable: false),
                    InstallationDate = table.Column<DateTime>(type: "datetime2(3)", nullable: false),
                    Latitude = table.Column<decimal>(type: "DECIMAL(9,6)", nullable: false),
                    Longitude = table.Column<decimal>(type: "DECIMAL(9,6)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerPlants", x => x.Id);
                    table.CheckConstraint("CK_PowerPlant_InstalledPower", "InstalledPower >= 0.1 AND InstalledPower <= 100");
                    table.CheckConstraint("CK_PowerPlant_Latitude", "Latitude >= -90 AND Latitude <= 90");
                    table.CheckConstraint("CK_PowerPlant_Longitude", "Longitude >= -180 AND Longitude <= 180");
                });

            migrationBuilder.CreateTable(
                name: "PowerProductions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PowerPlantId = table.Column<int>(type: "int", nullable: false),
                    PowerProduced = table.Column<float>(type: "real", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerProductions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PowerProductions_PowerPlants_PowerPlantId",
                        column: x => x.PowerPlantId,
                        principalTable: "PowerPlants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PowerProductions_PowerPlantId_Timestamp",
                table: "PowerProductions",
                columns: new[] { "PowerPlantId", "Timestamp" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PowerProductions");

            migrationBuilder.DropTable(
                name: "PowerPlants");
        }
    }
}
