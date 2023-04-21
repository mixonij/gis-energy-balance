using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using NpgsqlTypes;

#nullable disable

namespace Ispu.Gis.EnergyBalances.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CityDistrictAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<NpgsqlPoint>(
                name: "south_east_point",
                table: "cities",
                type: "point",
                nullable: false,
                defaultValueSql: "'(0,0)'::point",
                oldClrType: typeof(NpgsqlPoint),
                oldType: "point",
                oldDefaultValueSql: "'(0,0)'::point")
                .Annotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<NpgsqlPoint>(
                name: "north_west_point",
                table: "cities",
                type: "point",
                nullable: false,
                defaultValueSql: "'(0,0)'::point",
                oldClrType: typeof(NpgsqlPoint),
                oldType: "point",
                oldDefaultValueSql: "'(0,0)'::point")
                .Annotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<string>(
                name: "name_native",
                table: "cities",
                type: "text",
                nullable: false,
                defaultValueSql: "''::text",
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValueSql: "''::text")
                .Annotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "cities",
                type: "text",
                nullable: false,
                defaultValueSql: "''::text",
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValueSql: "''::text")
                .Annotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<int>(
                name: "min_zoom",
                table: "cities",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "cities",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                .Annotation("Relational:ColumnOrder", 0)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn);

            migrationBuilder.CreateTable(
                name: "city_districts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    city_id = table.Column<int>(type: "integer", nullable: false),
                    geometry = table.Column<MultiLineString>(type: "geometry(MultiLineString,4326)", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_city_districts_city_id",
                table: "city_districts",
                column: "city_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "city_districts");

            migrationBuilder.AlterColumn<NpgsqlPoint>(
                name: "south_east_point",
                table: "cities",
                type: "point",
                nullable: false,
                defaultValueSql: "'(0,0)'::point",
                oldClrType: typeof(NpgsqlPoint),
                oldType: "point",
                oldDefaultValueSql: "'(0,0)'::point")
                .OldAnnotation("Relational:ColumnOrder", 5);

            migrationBuilder.AlterColumn<NpgsqlPoint>(
                name: "north_west_point",
                table: "cities",
                type: "point",
                nullable: false,
                defaultValueSql: "'(0,0)'::point",
                oldClrType: typeof(NpgsqlPoint),
                oldType: "point",
                oldDefaultValueSql: "'(0,0)'::point")
                .OldAnnotation("Relational:ColumnOrder", 4);

            migrationBuilder.AlterColumn<string>(
                name: "name_native",
                table: "cities",
                type: "text",
                nullable: false,
                defaultValueSql: "''::text",
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValueSql: "''::text")
                .OldAnnotation("Relational:ColumnOrder", 3);

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "cities",
                type: "text",
                nullable: false,
                defaultValueSql: "''::text",
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValueSql: "''::text")
                .OldAnnotation("Relational:ColumnOrder", 2);

            migrationBuilder.AlterColumn<int>(
                name: "min_zoom",
                table: "cities",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Relational:ColumnOrder", 1);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "cities",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn)
                .OldAnnotation("Relational:ColumnOrder", 0);
        }
    }
}
