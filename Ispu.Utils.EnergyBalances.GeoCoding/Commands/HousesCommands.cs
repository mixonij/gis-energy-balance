using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Contexts;
using Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities;
using Ispu.Utils.EnergyBalances.GeoCoding.Options;
using Microsoft.EntityFrameworkCore;
using NpgsqlTypes;
using OsmSharp;
using OsmSharp.Complete;
using OsmSharp.Streams;
using Serilog;
using House = Ispu.Utils.EnergyBalances.GeoCoding.Models.House;

namespace Ispu.Utils.EnergyBalances.GeoCoding.Commands;

public class HousesCommands
{
    private static readonly ILogger _logger = Log.ForContext<HousesCommands>();

    public static async Task<int> ProcessRowsAsync(HousesOptions housesOptions)
    {
        var records = GetFileRecords(housesOptions);
        _logger.Information("Получено записей: {RecordsCount}", records.Count);


        await using var fileStream = File.OpenRead(housesOptions.PbfPath);
        var source = new PBFOsmStreamSource(fileStream);

        var completeSource = source.ToComplete();
        
        var houses = (from osmGeo in completeSource select osmGeo).Where(x => x.Type == OsmGeoType.Way
            && x.Tags.Any(y => y.Key is "addr:street" or "addr:housenumber")
            && x is CompleteWay).Select(x=>x as CompleteWay).ToList();
        
        var areas = (from osmGeo in completeSource select osmGeo).Where(x => x.Type == OsmGeoType.Way
                                                                             && x.Tags.Any(y => y.Key is "landuse" && y.Value == "residential")
                                                                             && x.Tags.Any(y=> y.Key is "residential" && y.Value is "urban" or "rural"
                                                                             && x is CompleteWay)).Select(x=> x as CompleteWay).ToList();
        

        var options = new DbContextOptionsBuilder<EnergyBalancesContext>().UseNpgsql(
            "Server=localhost;Port=5433;Database=energy_balances;UserId=postgres;Password=postgres;").Options;
        await using var dbContext = new EnergyBalancesContext(options);
        await dbContext.Database.MigrateAsync();

        var ivanovoCity = await dbContext.Cities.FirstOrDefaultAsync(x => x.NameRussian == "Иваново");
        if (ivanovoCity is null)
        {
            return 0;
        }

        foreach (var (way, index) in houses.Select((x, index)=>(x, index)))
        {
            var street = way.Tags.FirstOrDefault(x => x.Key == "addr:street").Value;
            var houseNumber = way.Tags.FirstOrDefault(x => x.Key == "addr:housenumber").Value;

            if (street is null || houseNumber is null)
            {
                continue;
            }

            var house = records.FirstOrDefault(x =>
                x.HouseNumber == houseNumber && (x.Street.Contains(street) || street.Contains(x.Street)));
            if (house is null || !house.QuartersCount.HasValue || !house.Area.HasValue)
            {
                continue;
            }

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

            await dbContext.SaveChangesAsync();

            var buildingInfo = new BuildingsInfo
            {
                BuildingId = building.Id,
                Area = house.Area.Value,
                ResidentsCount = house.QuartersCount.Value * 3,
            };
            
            await dbContext.BuildingsInfos.AddAsync(buildingInfo);
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