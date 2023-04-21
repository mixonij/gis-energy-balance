using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using NpgsqlTypes;

#nullable disable

namespace Ispu.Gis.EnergyBalances.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:fuzzystrmatch", ",,")
                .Annotation("Npgsql:PostgresExtension:postgis", ",,")
                .Annotation("Npgsql:PostgresExtension:tiger.postgis_tiger_geocoder", ",,")
                .Annotation("Npgsql:PostgresExtension:topology.postgis_topology", ",,");

            migrationBuilder.CreateTable(
                name: "cities",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name = table.Column<string>(type: "text", nullable: false, defaultValueSql: "''::text"),
                    name_native = table.Column<string>(type: "text", nullable: false, defaultValueSql: "''::text"),
                    north_west_point = table.Column<NpgsqlPoint>(type: "point", nullable: false, defaultValueSql: "'(0,0)'::point"),
                    south_east_point = table.Column<NpgsqlPoint>(type: "point", nullable: false, defaultValueSql: "'(0,0)'::point"),
                    min_zoom = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("cities_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Building",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CityId = table.Column<int>(type: "integer", nullable: false),
                    Coordinates = table.Column<NpgsqlPoint>(type: "point", nullable: false),
                    PolygonCoordinates = table.Column<NpgsqlPoint[]>(type: "point[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Building", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Building_cities_CityId",
                        column: x => x.CityId,
                        principalTable: "cities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HeatingStation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CityId = table.Column<int>(type: "integer", nullable: false),
                    NominalPower = table.Column<float>(type: "real", nullable: false),
                    Coords = table.Column<NpgsqlPoint>(type: "point", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeatingStation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HeatingStation_cities_CityId",
                        column: x => x.CityId,
                        principalTable: "cities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BuildingsInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BuildingId = table.Column<int>(type: "integer", nullable: false),
                    BuiltYear = table.Column<int>(type: "integer", nullable: true),
                    ResidentsCount = table.Column<int>(type: "integer", nullable: false),
                    Area = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuildingsInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuildingsInfo_Building_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Building",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "cities",
                columns: new[] { "id", "min_zoom", "name", "name_native", "north_west_point", "south_east_point" },
                values: new object[] { 1, 11, "Ivanovo", "Иваново", new NpgsqlTypes.NpgsqlPoint(57.093009000000002, 40.661774000000001), new NpgsqlTypes.NpgsqlPoint(56.903770999999999, 41.307220000000001) });

            migrationBuilder.CreateIndex(
                name: "IX_Building_CityId",
                table: "Building",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_BuildingsInfo_BuildingId",
                table: "BuildingsInfo",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_HeatingStation_CityId",
                table: "HeatingStation",
                column: "CityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuildingsInfo");

            migrationBuilder.DropTable(
                name: "HeatingStation");

            migrationBuilder.DropTable(
                name: "Building");

            migrationBuilder.DropTable(
                name: "cities");
        }
    }
}
