using System.Globalization;
using System.Runtime.InteropServices.JavaScript;
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
using OsmSharp;
using OsmSharp.Complete;
using OsmSharp.Streams;
using Serilog;

namespace Ispu.Utils.EnergyBalances.GeoCoding.Commands;

public class HousesCommands
{
    private static readonly ILogger _logger = Log.ForContext<HousesCommands>();

    public static async Task<int> ProcessRowsAsync(HousesOptions housesOptions)
    {
        var records = GetFileRecords(housesOptions);
        _logger.Information("Получено записей: {RecordsCount}", records.Count);


        await using var fileStream = File.OpenRead(housesOptions.PbfPath);
        // create source stream.
        var source = new PBFOsmStreamSource(fileStream);

        var completeSource = source.ToComplete();
        var filteredWays = (from osmGeo in completeSource select osmGeo).Where(x => x.Type == OsmGeoType.Way
            && x.Tags.Any(y => y.Key is "addr:street" or "addr:housenumber")
            && x is CompleteWay).Select(x=>x as CompleteWay).ToList();
        
        var areas = (from osmGeo in completeSource select osmGeo).Where(x => x.Type == OsmGeoType.Way
                                                                             && x.Tags.Any(y => y.Key is "landuse" && y.Value == "residential")
                                                                             && x.Tags.Any(y=> y.Key is "residential" && y.Value == "urban"
                                                                             && x is CompleteWay)).Select(x=> x as CompleteWay).ToList();
        

        var options = new DbContextOptionsBuilder<EnergyBalancesContext>().UseNpgsql(
            "Server=localhost;Port=5432;Database=energy_balances;UserId=postgres;Password=postgres;").Options;
        await using var dbContext = new EnergyBalancesContext(options);
        await dbContext.Database.MigrateAsync();

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("User-Agent", "Ispu.Utils.EnergyBalances.GeoCoding");

        var ivanovoCity = await dbContext.Cities.FirstOrDefaultAsync(x => x.NameRussian == "Иваново");
        if (ivanovoCity is null)
        {
            return 0;
        }

        foreach (var (way, index) in filteredWays.Select((x, index)=>(x, index)))
        {
            //var street = way.Tags.First(x => x.Key == "addr:street").Value;
            //var houseNumber = way.Tags.First(x => x.Key == "addr:housenumber").Value;

            var polygon = way.Nodes.Select(x => new NpgsqlPoint(x.Latitude.Value, x.Longitude.Value))
                .ToArray();

            var building = new Building
            {
                City = ivanovoCity,
                CityId = ivanovoCity.Id,
                PolygonCoordinates = polygon,
                Coordinates = new NpgsqlPoint(0,0)
            };
            
            await dbContext.Buildings.AddAsync(building);
        }

        foreach (var area in areas)
        { 
            var polygon = area.Nodes.Select(x => new NpgsqlPoint(x.Latitude.Value, x.Longitude.Value))
                .ToArray();
        
            var cityArea = new Area
            {
                CityId = ivanovoCity.Id,
                PolygonCoordinates = polygon,
            };
            
            await dbContext.Areas.AddAsync(cityArea);
        }
        

        foreach (var house in records)
        {
            // try
            // {
            //     var building = new Building
            //     {
            //         Id = house.Id,
            //         City = ivanovoCity,
            //         CityId = ivanovoCity.Id,
            //         Coordinates = new NpgsqlPoint(buildingProperties.Geometry.Coordinates[0],
            //             buildingProperties.Geometry.Coordinates[1])
            //     };
            //
            //     _logger.Information("Получен дом: {Building}", house.Address);
            //
            //     await dbContext.Buildings.AddAsync(building);
            //
            //     //await Task.Delay(200);
            // }
            // catch
            // {
            //     // Ничего не делаем
            // }
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