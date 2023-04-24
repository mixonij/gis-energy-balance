using Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Contexts;
using Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities;
using Ispu.Utils.EnergyBalances.GeoCoding.Context;
using Ispu.Utils.EnergyBalances.GeoCoding.Databases;
using Ispu.Utils.EnergyBalances.GeoCoding.Loaders;
using Ispu.Utils.EnergyBalances.GeoCoding.Options;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using OsmSharp;
using OsmSharp.Complete;
using Serilog;

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

        await using var pipesContext = new HeatingNetworkDbContext();

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
            var geometry = GetGeometry(area);

            var cityArea = new CityDistrict
            {
                CityId = city.Id,
                Geometry = geometry
            };

            await dbContext.CityDistricts.AddAsync(cityArea);
        }

        await dbContext.SaveChangesAsync();

        var buildings = osmNodes.Where(x => x.Type == OsmGeoType.Way
                                            && x.Tags.Any(y => y.Key is "addr:street" or "addr:housenumber"))
            .OfType<CompleteWay>().ToList();

        await dbContext.Buildings.AddRangeAsync(buildings.Select(GetGeometry).Select(geometry => new Building
        {
            Geometry = geometry
        }));

        await dbContext.SaveChangesAsync();

        var cityDistricts = await dbContext.CityDistricts.ToListAsync();

        foreach (var building in dbContext.Buildings)
        {
            var cityDistrict = cityDistricts.FirstOrDefault(x => x.Geometry.Contains(building.Geometry));
            if (cityDistrict is null)
            {
                continue;
            }

            building.CityId = cityDistrict.CityId;
            building.DistrictId = cityDistrict.Id;

            dbContext.Buildings.Update(building);
        }

        await dbContext.SaveChangesAsync();

        foreach (var heatingPipe in pipesContext.Heatingnetworkivs.Select(x => new HeatingPipe
                 {
                     DPod = x.Dpod!.Value,
                     DObr = x.Dobr!.Value,
                     Geometry = EF.Functions.Transform(x.WkbGeometry!, 4326)
                 }))
        {
            await dbContext.HeatingPipes.AddAsync(heatingPipe);
        }

        await dbContext.SaveChangesAsync();

        if (osmNodes.FirstOrDefault(x => x.Id == 168092602) is not CompleteWay ispuHeatingStation)
        {
            return await Task.FromResult(1);
        }

        var stationEntity = new HeatingStation
        {
            CityId = city.Id,
            Geometry = GetGeometry(ispuHeatingStation),
            NominalPower = 15.448,
            Name = "Котельная ИГЭУ"
        };

        await dbContext.HeatingStations.AddAsync(stationEntity);

        var stations = new List<(long OsmId, double Power, string Name)>
        {
            (109790361, 1.92, "Котельная №1,с.Богородское, ул.Б.Клинцевская, 2а"),
            (534327860, 0.86, "Котельная №2, м.Харинка, ул.Окуловой, 77"),
            (109793284, 0.32, "Котельная №3, м.Лесное, ул.Хвойная, 2"),
            (109793272, 0.32, "Котельная №17, м. Лесное, ул. 5-я Снежная, 3"),
            (681490591, 0.31, "Котельная №10, ул. Детская, 2/7"),
            (124158272, 0.74, "Котельная №18, ул.Свободы,1"),
            (109306337, 5.14, "Котельная №19, ул.Шувандиной,111"),
            (5318038222, 0.62, "Котельная №20 (паровая), ул.Любимова,5"),
            (565062254, 2.11, "Котельная №23,  ул.Садовского, 7"),
            (109559753, 0.48, "Котельная №24,  ул.Носова, 49"),
            (211102792, 0.107, "Котельная №30,  ул.Володиной, 7"),
            (135733884, 0, "Котельная №31,  ул.Лебедева-Кумача,10-б"),
            (87793352, 0, "Котельная №33,  ул.Авдотьинская, 20а"),
            (481991073, 1.01, "Котельная №35,  ул.Маршала Жаворонкова,40"),
            (702793799, 15.26, "Котельная №37,  ул.Полка Нормандия Неман, 103"),
            (534330829, 0.09, "Котельная №39,  м.Горино, 2-я Ягодная,31"),
            (112049837, 0.34, "Котельная №41,  ул.Сахарова, 56 (строение 1)"),
            (120642587, 0.10, "Котельная №43,  ул. 9-я Линия,д.1/6"),
            (427522880, 0.25, "Котельная №45,  ул. Красных Зорь, 28"),
            (438273974, 0, "Котельная №46,  ул. Красных Зорь, 50")
        };

        foreach (var station in stations)
        {
            if (osmNodes.FirstOrDefault(x => x.Id == station.OsmId) is not CompleteWay osmHeatingStation)
            {
                continue;
            }
            
            var osmStationEntity = new HeatingStation
            {
                CityId = city.Id,
                Geometry = GetGeometry(osmHeatingStation),
                NominalPower = station.Power,
                Name = station.Name
            };
        
            await dbContext.HeatingStations.AddAsync(osmStationEntity);
        }


        await dbContext.SaveChangesAsync();

        var pipes = GetPipes(dbContext, dbContext.HeatingStations.First().Geometry);

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

        //
        return await Task.FromResult(1);
    }

    private static List<HeatingPipe> GetPipes(CityEnergyModelingContext cityEnergyModelingContext,
        Polygon stationEntityGeometry)
    {
        stationEntityGeometry.SRID = 4326;
        var pipesConnectedToHeating =
            cityEnergyModelingContext.HeatingPipes.Where(x => stationEntityGeometry.Intersects(x.Geometry)).ToList();

        return pipesConnectedToHeating;
    }

    private static Polygon GetGeometry(CompleteWay completeWay)
    {
        var points = completeWay.Nodes;

        var linearRing =
            new LinearRing(points.Select(x => new Coordinate(x.Longitude!.Value, x.Latitude!.Value)).ToArray());

        // List<LineString> lines = new();
        // for (var i = 0; i < points.Length; i++)
        // {
        //     if (i == points.Length - 1)
        //     {
        //         break;
        //     }
        //
        //     var startPoint = points[i];
        //     var endPoint = points[i + 1];
        //
        //     var coordinates = new Coordinate[]
        //     {
        //         new(startPoint.Longitude!.Value, startPoint.Latitude!.Value),
        //         new(endPoint.Longitude!.Value, endPoint.Latitude!.Value)
        //     };
        //
        //     var lr = new LinearRing(coordinates);
        //
        //
        //     lines.Add(new LineString(coordinates));
        // }


        var geometry = new Polygon(linearRing);
        return geometry;
    }
}