using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

#nullable disable

namespace Ispu.Gis.EnergyBalances.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewCityModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "cities");

            migrationBuilder.AlterColumn<string>(
                name: "name_russian",
                table: "cities",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<NpgsqlPoint>(
                name: "north_west_bound",
                table: "cities",
                type: "point",
                nullable: false,
                defaultValue: new NpgsqlTypes.NpgsqlPoint(0.0, 0.0));

            migrationBuilder.AddColumn<NpgsqlPoint>(
                name: "south_east_bound",
                table: "cities",
                type: "point",
                nullable: false,
                defaultValue: new NpgsqlTypes.NpgsqlPoint(0.0, 0.0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "north_west_bound",
                table: "cities");

            migrationBuilder.DropColumn(
                name: "south_east_bound",
                table: "cities");

            migrationBuilder.AlterColumn<string>(
                name: "name_russian",
                table: "cities",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "cities",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
