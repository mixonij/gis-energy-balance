﻿using Ispu.Gis.EnergyBalances.Application.Models;
using Ispu.Gis.EnergyBalances.Application.Storages;
using Ispu.Gis.EnergyBalances.Domain.Entities;
using Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Contexts;
using Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Building = Ispu.Gis.EnergyBalances.Domain.Entities.Building;

namespace Ispu.Gis.EnergyBalances.Controlles;

[ApiController]
[Route("api/geodata")]
public class GeoDataController : ControllerBase
{
    private readonly EnergyBalancesContext _db;
    private readonly IPipesStorage _pipesStorage;

    public GeoDataController(EnergyBalancesContext db, IPipesStorage pipesStorage)
    {
        _db = db;
        _pipesStorage = pipesStorage;
    }

    [HttpGet("[action]/{cityId}")]
    public Task<List<Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities.Building>> GetHouses(int cityId)
    {
        return _db.Buildings.Where(x => x.CityId == cityId).ToListAsync();
    }

    [HttpGet("[action]")]
    public Task<List<City>> GetCities()
    {
        return _db.Cities.ToListAsync();
    }

    [HttpGet("[action]/{cityId}")]
    public Task<List<Area>> GetAreas(int cityId)
    {
        return _db.Areas.Where(x => x.CityId == cityId).ToListAsync();
    }

    [HttpGet("[action]/{buildingId}")]
    public Task<BuildingsInfo?> GetBuildingInfo(int buildingId)
    {
        return _db.BuildingsInfos.FirstOrDefaultAsync(x => x.BuildingId == buildingId);
    }

    [HttpGet("[action]/{buildingId}")]
    public async Task<BuildingPowerConnections> CalculateEnergyBalance(int buildingId)
    {
        var buildingInfo = await _db.BuildingsInfos.FirstAsync(x => x.BuildingId == buildingId);
        var building = new Building
        {
            LivingSquare = buildingInfo.Area,
            ResidentsCount = buildingInfo.ResidentsCount
        };

        var pC = new BuildingPowerConnections(building);

        return pC;
    }

    [HttpGet("[action]")]
    public async Task<List<Pipe>> GetPipeGroups()
    {
        return (await _pipesStorage.GetPipes()).Select(x => new Pipe
        {
            Id = x.OgcFid,
            DOut = x.Dobr,
            DIn = x.Dpod,
            Length = x.ShapeLeng,
            Points = x.WkbGeometry!.Coordinates.Select(t=> new GeographyPoint(t)).ToList()
        }).ToList();
    }
    
    [HttpGet("[action]")]
    public Task<List<HeatingStation>> GetHeatingStations()
    {
        return _db.HeatingStations.ToListAsync();
    }
}