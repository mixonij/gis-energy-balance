using System.Globalization;
using System.Text.Json;
using System.Web;
using CsvHelper;
using CsvHelper.Configuration;
using Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Contexts;
using Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities;
using Ispu.Utils.EnergyBalances.GeoCoding.Models;
using Ispu.Utils.EnergyBalances.GeoCoding.Options;
using Microsoft.EntityFrameworkCore;
using NpgsqlTypes;
using Serilog;

namespace Ispu.Utils.EnergyBalances.GeoCoding.Commands;

public class HousesCommands
{
    private static readonly ILogger _logger = Log.ForContext<HousesCommands>();

    public static async Task<int> ProcessRowsAsync(HousesOptions housesOptions)
    {
        var records = GetFileRecords(housesOptions);
        _logger.Information("Получено записей: {RecordsCount}", records.Count);

        var options = new DbContextOptionsBuilder<EnergyBalancesContext>().UseNpgsql(
            "Server=localhost;Port=5432;Database=energy_balances;UserId=postgres;Password=postgres;").Options;
        await using var dbContext = new EnergyBalancesContext(options);
        await dbContext.Database.MigrateAsync();

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("User-Agent", "Ispu.Utils.EnergyBalances.GeoCoding");

        var ivanovoCity = await dbContext.Cities.FirstOrDefaultAsync(x => x.Name == "Ivanovo");
        if (ivanovoCity is null)
        {
            return 0;
        }

        foreach (var house in records)
        {
            try
            {
                var response = await httpClient.GetAsync(
                    $"https://nominatim.openstreetmap.org/search?street={house.Street} {house.ShortNameStreet} {house.HouseNumber}&city={house.City}&country=Russia&format=geojson");
                if (!response.IsSuccessStatusCode)
                {
                    //await Task.Delay(1000);
                    continue;
                }

                var responseStream = await response.Content.ReadAsStreamAsync();
                var geoJsonBuilding = await JsonSerializer.DeserializeAsync<GeoJsonBuildingResponse>(responseStream);
                if (geoJsonBuilding is null || !geoJsonBuilding.Features.Any())
                {
                    //await Task.Delay(1000);
                    continue;
                }

                var buildingProperties = geoJsonBuilding.Features.First();
                if (buildingProperties.Properties.Type != "apartments")
                {
                    //await Task.Delay(200);
                    continue;
                }

                var building = new Building
                {
                    Id = house.Id,
                    City = ivanovoCity,
                    CityId = ivanovoCity.Id,
                    Coordinates = new NpgsqlPoint(buildingProperties.Geometry.Coordinates[0],
                        buildingProperties.Geometry.Coordinates[1])
                };

                _logger.Information("Получен дом: {Building}", house.Address);

                await dbContext.Buildings.AddAsync(building);

                //await Task.Delay(200);
            }
            catch
            {
                // Ничего не делаем
            }
        }

        await dbContext.SaveChangesAsync();

        return await Task.FromResult(1);
    }

    private static List<House> GetFileRecords(HousesOptions housesOptions)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            HeaderValidated = null,
            MissingFieldFound = null,
            BadDataFound = null,
            Delimiter = ";"
        };
        using var reader = new StreamReader(housesOptions.CsvPath);
        using var csv = new CsvReader(reader, config);
        return csv.GetRecords<House>().Skip(964).Take(3565).ToList();
    }
}