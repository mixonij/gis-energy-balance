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
                name: "areas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    city_id = table.Column<int>(type: "integer", nullable: false),
                    polygon_coordinates = table.Column<NpgsqlPoint[]>(type: "point[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("areas_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cities",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    name_russian = table.Column<string>(type: "text", nullable: false, defaultValueSql: "''::text"),
                    north_west_bound = table.Column<NpgsqlPoint>(type: "point", nullable: false, defaultValueSql: "'(0,0)'::point"),
                    south_east_bound = table.Column<NpgsqlPoint>(type: "point", nullable: false, defaultValueSql: "'(0,0)'::point"),
                    min_zoom = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("cities_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "heatingnetworkiv",
                columns: table => new
                {
                    ogc_fid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    wkb_geometry = table.Column<MultiLineString>(type: "geometry(MultiLineString,3857)", nullable: true),
                    objectid = table.Column<decimal>(type: "numeric(10)", precision: 10, nullable: true),
                    sys = table.Column<decimal>(type: "numeric(19,11)", precision: 19, scale: 11, nullable: true),
                    dpod = table.Column<decimal>(type: "numeric(19,11)", precision: 19, scale: 11, nullable: true),
                    dobr = table.Column<decimal>(type: "numeric(19,11)", precision: 19, scale: 11, nullable: true),
                    shape_leng = table.Column<decimal>(type: "numeric(19,11)", precision: 19, scale: 11, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("heatingnetworkiv_pk", x => x.ogc_fid);
                });

            migrationBuilder.CreateTable(
                name: "houses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    coords = table.Column<NpgsqlPoint>(type: "point", nullable: false),
                    description = table.Column<string>(type: "jsonb", nullable: true),
                    address = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("houses_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "buildings",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    city_id = table.Column<int>(type: "integer", nullable: false),
                    coordinates = table.Column<NpgsqlPoint>(type: "point", nullable: false),
                    polygon_coordinates = table.Column<NpgsqlPoint[]>(type: "point[]", nullable: false, defaultValueSql: "ARRAY[]::point[]")
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.id);
                    table.ForeignKey(
                        name: "city_id",
                        column: x => x.city_id,
                        principalTable: "cities",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "heating_stations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    city_id = table.Column<int>(type: "integer", nullable: false),
                    nominal_power = table.Column<float>(type: "real", nullable: false, defaultValueSql: "1"),
                    coords = table.Column<NpgsqlPoint>(type: "point", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("heating_stations_pkey", x => x.id);
                    table.ForeignKey(
                        name: "city_id",
                        column: x => x.city_id,
                        principalTable: "cities",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "buildings_info",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    building_id = table.Column<int>(type: "integer", nullable: false),
                    built_year = table.Column<int>(type: "integer", nullable: true),
                    residents_count = table.Column<int>(type: "integer", nullable: false),
                    area = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("buildings_info_pkey", x => x.id);
                    table.ForeignKey(
                        name: "building_id",
                        column: x => x.building_id,
                        principalTable: "buildings",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_buildings_city_id",
                table: "buildings",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "IX_buildings_info_building_id",
                table: "buildings_info",
                column: "building_id");

            migrationBuilder.CreateIndex(
                name: "IX_heating_stations_city_id",
                table: "heating_stations",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "heatingnetworkiv_wkb_geometry_geom_idx",
                table: "heatingnetworkiv",
                column: "wkb_geometry")
                .Annotation("Npgsql:IndexMethod", "gist");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "areas");

            migrationBuilder.DropTable(
                name: "buildings_info");

            migrationBuilder.DropTable(
                name: "heating_stations");

            migrationBuilder.DropTable(
                name: "heatingnetworkiv");

            migrationBuilder.DropTable(
                name: "houses");

            migrationBuilder.DropTable(
                name: "buildings");

            migrationBuilder.DropTable(
                name: "cities");
        }
    }
}
