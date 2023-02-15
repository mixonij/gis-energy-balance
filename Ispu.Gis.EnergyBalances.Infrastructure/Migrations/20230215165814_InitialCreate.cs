using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using NpgsqlTypes;

#nullable disable

namespace Ispu.Gis.EnergyBalances.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "areas",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    cityid = table.Column<int>(name: "city_id", type: "integer", nullable: false),
                    polygoncoordinates = table.Column<NpgsqlPoint[]>(name: "polygon_coordinates", type: "point[]", nullable: false)
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
                    namerussian = table.Column<string>(name: "name_russian", type: "text", nullable: false, defaultValueSql: "''::text"),
                    northwestbound = table.Column<NpgsqlPoint>(name: "north_west_bound", type: "point", nullable: false, defaultValueSql: "'(0,0)'::point"),
                    southeastbound = table.Column<NpgsqlPoint>(name: "south_east_bound", type: "point", nullable: false, defaultValueSql: "'(0,0)'::point"),
                    minzoom = table.Column<int>(name: "min_zoom", type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("cities_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "buildings",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    cityid = table.Column<int>(name: "city_id", type: "integer", nullable: false),
                    coordinates = table.Column<NpgsqlPoint>(type: "point", nullable: false),
                    polygoncoordinates = table.Column<NpgsqlPoint[]>(name: "polygon_coordinates", type: "point[]", nullable: false, defaultValueSql: "ARRAY[]::point[]")
                },
                constraints: table =>
                {
                    table.PrimaryKey("id", x => x.id);
                    table.ForeignKey(
                        name: "city_id",
                        column: x => x.cityid,
                        principalTable: "cities",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "buildings_info",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    buildingid = table.Column<int>(name: "building_id", type: "integer", nullable: false),
                    builtyear = table.Column<int>(name: "built_year", type: "integer", nullable: true),
                    residentscount = table.Column<int>(name: "residents_count", type: "integer", nullable: false),
                    area = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("buildings_info_pkey", x => x.id);
                    table.ForeignKey(
                        name: "building_id",
                        column: x => x.buildingid,
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "areas");

            migrationBuilder.DropTable(
                name: "buildings_info");

            migrationBuilder.DropTable(
                name: "buildings");

            migrationBuilder.DropTable(
                name: "cities");
        }
    }
}
