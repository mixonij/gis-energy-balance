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
                name: "heating_stations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    nominal_power = table.Column<double>(type: "double precision", nullable: false),
                    city_id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    geometry = table.Column<Polygon>(type: "geometry(Polygon,4326)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("heating_stations_pkey", x => x.id);
                    table.ForeignKey(
                        name: "FK_heating_stations_cities_city_id",
                        column: x => x.city_id,
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
                name: "heating_pipes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    d_pod = table.Column<decimal>(type: "numeric", nullable: false),
                    d_obr = table.Column<decimal>(type: "numeric", nullable: false),
                    heating_station_id = table.Column<int>(type: "integer", nullable: true),
                    geometry = table.Column<MultiLineString>(type: "geometry(MultiLineString,4326)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("heating_pipes_pkey", x => x.id);
                    table.ForeignKey(
                        name: "FK_heating_pipes_heating_stations_heating_station_id",
                        column: x => x.heating_station_id,
                        principalTable: "heating_stations",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "building_information",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    building_id = table.Column<int>(type: "integer", nullable: false),
                    built_year = table.Column<int>(type: "integer", nullable: true),
                    residents_count = table.Column<int>(type: "integer", nullable: false),
                    residential_area = table.Column<double>(type: "double precision", nullable: false),
                    registry_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("building_information_pkey", x => x.id);
                    table.ForeignKey(
                        name: "FK_building_information_buildings_building_id",
                        column: x => x.building_id,
                        principalTable: "buildings",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "cities",
                columns: new[] { "id", "min_zoom", "name", "name_native", "north_west_point", "south_east_point" },
                values: new object[] { 1, 11, "Ivanovo", "Иваново", new NpgsqlTypes.NpgsqlPoint(57.093009000000002, 40.661774000000001), new NpgsqlTypes.NpgsqlPoint(56.903770999999999, 41.307220000000001) });

            migrationBuilder.CreateIndex(
                name: "IX_building_information_building_id",
                table: "building_information",
                column: "building_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_building_information_id",
                table: "building_information",
                column: "id");

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
                name: "IX_city_districts_city_id",
                table: "city_districts",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "IX_heating_pipes_heating_station_id",
                table: "heating_pipes",
                column: "heating_station_id");

            migrationBuilder.CreateIndex(
                name: "IX_heating_pipes_id",
                table: "heating_pipes",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_heating_stations_city_id",
                table: "heating_stations",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "IX_heating_stations_id",
                table: "heating_stations",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "building_information");

            migrationBuilder.DropTable(
                name: "heating_pipes");

            migrationBuilder.DropTable(
                name: "buildings");

            migrationBuilder.DropTable(
                name: "heating_stations");

            migrationBuilder.DropTable(
                name: "city_districts");

            migrationBuilder.DropTable(
                name: "cities");
        }
    }
}
