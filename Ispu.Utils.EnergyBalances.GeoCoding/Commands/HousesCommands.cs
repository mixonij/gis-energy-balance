using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Contexts;
using Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities;
using Ispu.Utils.EnergyBalances.GeoCoding.Databases;
using Ispu.Utils.EnergyBalances.GeoCoding.Loaders;
using Ispu.Utils.EnergyBalances.GeoCoding.Options;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using NpgsqlTypes;
using OsmSharp;
using OsmSharp.Complete;
using OsmSharp.Streams;
using Serilog;
using House = Ispu.Utils.EnergyBalances.GeoCoding.Models.House;

namespace Ispu.Utils.EnergyBalances.GeoCoding.Commands;

public class HousesCommands
{
    private static readonly ILogger Logger = Log.ForContext<HousesCommands>();

    public static async Task<int> ProcessRowsAsync(HousesOptions housesOptions)
    {
        // Загружаем список домов из CSV
        var csvHouses = CsvLoader.LoadData(housesOptions.CsvPath);
        Logger.Information("Загружено данных адресного реестра: {AddressesCount}", csvHouses.Count);

        // Узлы OSM
        var osmNodes = PbfLoader.LoadData(housesOptions.PbfPath);
        Logger.Information("Загружено данных OSM: {OsmNodesCount}", osmNodes.Count);

        // Получаем контекст БД
        await using var dbContext = await ContextLoader.GetContextAsync(housesOptions.PgConnectionString);

        var city = await dbContext.Cities.FirstOrDefaultAsync(x => x.Name == "Ivanovo");
        if (city is null)
        {
            throw new Exception("Не удалось получить город из базы данных");
        }

        var areas = osmNodes.Where(x => x.Type == OsmGeoType.Way
                                        && x.Tags.Any(y => y.Key is "landuse" && y.Value == "residential")
                                        && x.Tags.Any(y => y.Key is "residential" && y.Value is "urban" or "rural"))
            .OfType<CompleteWay>().ToList();

        foreach (var area in areas)
        {
            var points = area.Nodes;
            List<LineString> lines = new();
            for (var i = 0; i < points.Length; i++)
            {
                if (i == points.Length - 1)
                {
                    break;
                }

                var startPoint = points[i];
                var endPoint = points[i + 1];

                var coordinates = new Coordinate[]
                {
                    new(startPoint.Longitude!.Value, startPoint.Latitude!.Value),
                    new(endPoint.Longitude!.Value, endPoint.Latitude!.Value)
                };

                lines.Add(new LineString(coordinates));
            }

            var cityArea = new CityDistrict
            {
                CityId = city.Id,
                Geometry = new MultiLineString(lines.ToArray())
            };

            await dbContext.CityDistricts.AddAsync(cityArea);
        }

        // var options = new DbContextOptionsBuilder<EnergyBalancesContext>().UseNpgsql(
        //     "Server=localhost;Port=5433;Database=energy_balances;UserId=postgres;Password=postgres;").Options;
        // await using var dbContext = new EnergyBalancesContext(options);
        // await dbContext.Database.MigrateAsync();


        // var records = GetFileRecords(housesOptions);
        // _logger.Information("Получено записей: {RecordsCount}", records.Count);
        //
        //

        // var houses = osmNodes.Where(x => x.Type == OsmGeoType.Way
        //                                  && x.Tags.Any(y => y.Key is "addr:street" or "addr:housenumber")
        //                                  && x is CompleteWay).Select(x=>x as CompleteWay).ToList();
        //
        // var areas = (from osmGeo in completeSource select osmGeo).Where(x => x.Type == OsmGeoType.Way
        //                                                                      && x.Tags.Any(y => y.Key is "landuse" && y.Value == "residential")
        //                                                                      && x.Tags.Any(y=> y.Key is "residential" && y.Value is "urban" or "rural"
        //                                                                      && x is CompleteWay)).Select(x=> x as CompleteWay).ToList();
        //
        //

        //
        // var ivanovoCity = await dbContext.Cities.FirstOrDefaultAsync(x => x.NativeName == "Иваново");
        // if (ivanovoCity is null)
        // {
        //     ivanovoCity = new City
        //     {
        //         MinZoom = 11,
        //         NativeName = "Иваново",
        //         NorthWestPoint = new NpgsqlPoint(57.093009, 40.661774),
        //         SouthEastPoint = new NpgsqlPoint(56.903771, 41.30722)
        //     };
        //     
        //     await dbContext.Cities.AddAsync(ivanovoCity);
        //     await dbContext.SaveChangesAsync();
        //     return 0;
        // }
        //
        // foreach (var (way, index) in houses.Select((x, index)=>(x, index)))
        // {
        //     var street = way.Tags.FirstOrDefault(x => x.Key == "addr:street").Value;
        //     var houseNumber = way.Tags.FirstOrDefault(x => x.Key == "addr:housenumber").Value;
        //
        //     if (street is null || houseNumber is null)
        //     {
        //         continue;
        //     }
        //
        //     var house = records.FirstOrDefault(x =>
        //         x.HouseNumber == houseNumber && (x.Street.Contains(street) || street.Contains(x.Street)));
        //     if (house is null || !house.QuartersCount.HasValue || !house.Area.HasValue)
        //     {
        //         continue;
        //     }
        //
        //     var polygon = way.Nodes.Select(x => new NpgsqlPoint(x.Latitude.Value, x.Longitude.Value))
        //         .ToArray();
        //
        //     var building = new Building
        //     {
        //         City = ivanovoCity,
        //         CityId = ivanovoCity.Id,
        //         PolygonCoordinates = polygon,
        //         Coordinates = new NpgsqlPoint(0,0)
        //     };
        //     
        //     await dbContext.Buildings.AddAsync(building);
        //
        //     await dbContext.SaveChangesAsync();
        //
        //     var buildingInfo = new BuildingsInfo
        //     {
        //         BuildingId = building.Id,
        //         Area = house.Area.Value,
        //         ResidentsCount = house.QuartersCount.Value * 3,
        //     };
        //     
        //     await dbContext.BuildingsInfos.AddAsync(buildingInfo);
        // }
        //
        // foreach (var area in areas)
        // { 
        //     var polygon = area.Nodes.Select(x => new NpgsqlPoint(x.Latitude.Value, x.Longitude.Value))
        //         .ToArray();
        //
        //     var cityArea = new CityDistrict
        //     {
        //         CityId = ivanovoCity.Id,
        //         PolygonCoordinates = polygon,
        //     };
        //     
        //     await dbContext.Areas.AddAsync(cityArea);
        // }
        //
        await dbContext.SaveChangesAsync();
        //
        return await Task.FromResult(1);
    }
}