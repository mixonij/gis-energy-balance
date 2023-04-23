using AutoMapper;
using Ispu.Gis.EnergyBalances.Application.Models;
using Ispu.Gis.EnergyBalances.Application.Storages;
using Ispu.Gis.EnergyBalances.Domain.Entities;
using Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ispu.Gis.EnergyBalances.Controlles;

[ApiController]
[Route("api/geodata")]
public class GeoDataController : ControllerBase
{
    //private readonly EnergyBalancesContext _db;
    private readonly IPipesStorage _pipesStorage;
    private readonly CityEnergyModelingContext _newDb;
    private readonly IMapper _mapper;

    public GeoDataController(IPipesStorage pipesStorage, CityEnergyModelingContext newDb, IMapper mapper)
    {
        //_db = db;
        _pipesStorage = pipesStorage;
        _newDb = newDb;
        _mapper = mapper;
    }

    // [HttpGet("[action]/{cityId}")]
    // public Task<List<Building>> GetHouses(int cityId)
    // {
    //     return _newDb.Buildings.Select(x => _mapper.Map<Building>(x)).ToListAsync();
    // }

    [HttpGet("[action]")]
    public async Task<List<City>> GetCities()
    {
        var cities = await _newDb.Cities.Select(x => _mapper.Map<City>(x)).ToListAsync();
        return cities;
    }

    [HttpGet("[action]/{cityId}")]
    public async Task<List<CityDistrict>> GetAreas(int cityId)
    {
        var districts = await _newDb.CityDistricts.Where(x => x.CityId == cityId)
            .Include(x => x.Buildings)
            .Select(x => _mapper.Map<CityDistrict>(x))
            .ToListAsync();
        return districts;
    }

    // [HttpGet("[action]/{buildingId}")]
    // public Task<BuildingsInfo?> GetBuildingInfo(int buildingId)
    // {
    //     return _db.BuildingsInfos.FirstOrDefaultAsync(x => x.BuildingId == buildingId);
    // }
    //
    // [HttpGet("[action]/{buildingId}")]
    // public async Task<BuildingPowerConnections> CalculateEnergyBalance(int buildingId)
    // {
    //     var buildingInfo = await _db.BuildingsInfos.FirstAsync(x => x.BuildingId == buildingId);
    //     var building = new Building
    //     {
    //         LivingSquare = buildingInfo.Area,
    //         ResidentsCount = buildingInfo.ResidentsCount
    //     };
    //
    //     var pC = new BuildingPowerConnections(building);
    //
    //     return pC;
    // }

    [HttpGet("[action]")]
    public async Task<List<Pipe>> GetPipeGroups()
    {
        return (await _pipesStorage.GetPipes()).Select(x => new Pipe
        {
            Id = x.OgcFid,
            DOut = x.Dobr,
            DIn = x.Dpod,
            Length = x.ShapeLeng,
            Points = x.WkbGeometry!.Coordinates.Select(t => new GeographyPoint(t)).ToList()
        }).ToList();
    }

    // [HttpGet("[action]")]
    // public Task<List<HeatingStation>> GetHeatingStations()
    // {
    //     return _db.HeatingStations.ToListAsync();
    // }
}