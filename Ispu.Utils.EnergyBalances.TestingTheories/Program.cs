using Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Contexts;
using Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

var options = new DbContextOptionsBuilder<EnergyBalancesContext>().UseNpgsql(
    "Server=localhost;Port=5433;Database=energy_balances;UserId=postgres;Password=postgres;").Options;
await using var dbContext = new EnergyBalancesContext(options);

var coords = dbContext.Heatingnetworkivs.ToDictionary(x => x.OgcFid);

List<List<Heatingnetworkiv?>> groups = new();

while (coords.Any())
{
    var item = coords.First();
    coords.Remove(item.Key);

    List<Heatingnetworkiv?> group = new()
    {
        item.Value
    };


    var connectedPipes = GetPipes(item.Value);
    group.AddRange(connectedPipes);
    
    groups.Add(group.DistinctBy(x=>x.OgcFid).ToList());

    if (group.Any(x => x.OgcFid == 218))
    {
        var d = 1;
    }
}

var a = 1;


List<Heatingnetworkiv> GetPipes(Heatingnetworkiv pipe)
{
    List<Heatingnetworkiv> result = new();

    var connectedPipes = coords!.Values.Where(x => x.WkbGeometry!.Touches(pipe.WkbGeometry)).ToList();
    if (!connectedPipes.Any())
    {
        return new List<Heatingnetworkiv>();
    }
    
    foreach (var connectedPipe in connectedPipes)
    {
        coords.Remove(connectedPipe.OgcFid);

        result.Add(connectedPipe);
        result.AddRange(GetPipes(connectedPipe));
    }

    return result;
}