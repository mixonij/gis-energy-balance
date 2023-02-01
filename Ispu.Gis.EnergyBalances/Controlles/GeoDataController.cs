using Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Contexts;
using Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ispu.Gis.EnergyBalances.Controlles;

[ApiController]
[Route("api/geodata")]
public class GeoDataController: ControllerBase
{
    private readonly EnergyBalancesContext _db;

    public GeoDataController(EnergyBalancesContext db)
    {
        _db = db;
    }

    [HttpGet("[action]")]
    public Task<List<Building>> GetHouses()
    {
        return _db.Buildings.ToListAsync();
    }
}