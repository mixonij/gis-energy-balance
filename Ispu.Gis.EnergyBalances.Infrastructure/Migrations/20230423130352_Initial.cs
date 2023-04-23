using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;
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
                    min_zoom = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false, defaultValueSql: "''::text"),
                    name_native = table.Column<string>(type: "text", nullable: false, defaultValueSql: "''::text"),
                    north_west_point = table.Column<NpgsqlPoint>(type: "point", nullable: false, defaultValueSql: "'(0,0)'::point"),
                    south_east_point = table.Column<NpgsqlPoint>(type: "point", nullable: false, defaultValueSql: "'(0,0)'::point")
                },
                constraints: table =>
                {
                    table.PrimaryKey("cities_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "city_districts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    city_id = table.Column<int>(type: "integer", nullable: false),
                    geometry = table.Column<Polygon>(type: "geometry(Polygon,4326)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("areas_pkey", x => x.id);
                    table.ForeignKey(
                        name: "FK_city_districts_cities_city_id",
                        column: x => x.city_id,
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
                name: "buildings",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    city_id = table.Column<int>(type: "integer", nullable: true),
                    district_id = table.Column<int>(type: "integer", nullable: true),
                    geometry = table.Column<Polygon>(type: "geometry(Polygon,4326)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("buildings_pkey", x => x.id);
                    table.ForeignKey(
                        name: "FK_buildings_cities_city_id",
                        column: x => x.city_id,
                        principalTable: "cities",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_buildings_city_districts_district_id",
                        column: x => x.district_id,
                        principalTable: "city_districts",
                        principalColumn: "id");
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
                        name: "FK_BuildingsInfo_buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "buildings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "cities",
                columns: new[] { "id", "min_zoom", "name", "name_native", "north_west_point", "south_east_point" },
                values: new object[] { 1, 11, "Ivanovo", "Иваново", new NpgsqlTypes.NpgsqlPoint(57.093009000000002, 40.661774000000001), new NpgsqlTypes.NpgsqlPoint(56.903770999999999, 41.307220000000001) });

            migrationBuilder.CreateIndex(
                name: "IX_buildings_city_id",
                table: "buildings",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "IX_buildings_district_id",
                table: "buildings",
                column: "district_id");

            migrationBuilder.CreateIndex(
                name: "IX_buildings_id",
                table: "buildings",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_BuildingsInfo_BuildingId",
                table: "BuildingsInfo",
                column: "BuildingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_city_districts_city_id",
                table: "city_districts",
                column: "city_id");

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
                name: "buildings");

            migrationBuilder.DropTable(
                name: "city_districts");

            migrationBuilder.DropTable(
                name: "cities");
        }
    }
}
