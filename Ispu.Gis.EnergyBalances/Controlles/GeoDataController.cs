using Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Contexts;
using Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ispu.Gis.EnergyBalances.Controlles;

[ApiController]
[Route("api/geodata")]
public class GeoDataController : ControllerBase
{
    private readonly EnergyBalancesContext _db;

    public GeoDataController(EnergyBalancesContext db)
    {
        _db = db;
    }

    [HttpGet("[action]/{cityId}")]
    public Task<List<Building>> GetHouses(int cityId)
    {
        return _db.Buildings.Where(x => x.CityId == cityId).ToListAsync();
    }

    [HttpGet("[action]")]
    public Task<List<City>> GetCities()
    {
        return _db.Cities.ToListAsync();
    }
}