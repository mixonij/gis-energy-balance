// using Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Contexts;
// using Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.DependencyInjection;
//
// namespace Ispu.Gis.EnergyBalances.Application.Storages;
//
// public class PipesStorage : IPipesStorage
// {
//     private readonly IServiceScopeFactory _scopeFactory;
//     private List<List<Heatingnetworkiv?>> _groups;
//
//     public PipesStorage(IServiceScopeFactory scopeFactory)
//     {
//         _scopeFactory = scopeFactory;
//     }
//
//     public List<IEnumerable<Heatingnetworkiv>> GetAllPipes()
//     {
//         return _groups.Select(x => x.Select(y => new Heatingnetworkiv(y))).ToList();
//     }
//
//
//     public async Task<List<Heatingnetworkiv>> GetPipes()
//     {
//         using var scope = _scopeFactory.CreateScope();
//
//         await using var db = scope.ServiceProvider.GetRequiredService<EnergyBalancesContext>();
//
//         return new List<Heatingnetworkiv>();
//
//         // return await db.Heatingnetworkivs.Select(x => new Heatingnetworkiv
//         // {
//         //     OgcFid = x.OgcFid,
//         //     WkbGeometry = EF.Functions.Transform(x.WkbGeometry!, 4326),
//         //     Objectid = x.Objectid,
//         //     Sys = x.Sys,
//         //     Dpod = x.Dpod,
//         //     Dobr = x.Dobr,
//         //     ShapeLeng = x.ShapeLeng
//         // }).ToListAsync();
//     }
//
//     public async Task Initialize()
//     {
//         using var scope = _scopeFactory.CreateScope();
//
//         await using var db = scope.ServiceProvider.GetRequiredService<EnergyBalancesContext>();
//         
//         // var coords = await db.Heatingnetworkivs.Select(x=> new Heatingnetworkiv
//         // {
//         //     OgcFid = x.OgcFid,
//         //     WkbGeometry = EF.Functions.Transform(x.WkbGeometry!, 4326),
//         //     Objectid = x.Objectid,
//         //     Sys = x.Sys,
//         //     Dpod = x.Dpod,
//         //     Dobr = x.Dobr,
//         //     ShapeLeng = x.ShapeLeng
//         // }).ToDictionaryAsync(x => x.OgcFid);
//
//         List<List<Heatingnetworkiv?>> groups = new();
//
//         // while (coords.Any())
//         // {
//         //     var item = coords.First();
//         //     coords.Remove(item.Key);
//         //
//         //     List<Heatingnetworkiv?> group = new()
//         //     {
//         //         item.Value
//         //     };
//         //
//         //     var connectedPipes = await GetPipesAsync(coords, item.Value);
//         //     group.AddRange(connectedPipes);
//         //
//         //     groups.Add(group.DistinctBy(x => x.OgcFid).ToList());
//         // }
//
//         _groups = groups;
//     }
//
//
//     private async Task<List<Heatingnetworkiv>> GetPipesAsync(IDictionary<int, Heatingnetworkiv> coords,
//         Heatingnetworkiv pipe)
//     {
//         List<Heatingnetworkiv> result = new();
//
//         var connectedPipes = coords.Values.Where(x => 
//             x.WkbGeometry!.Touches(pipe.WkbGeometry) || x.WkbGeometry!.Intersects(pipe.WkbGeometry)).ToList();
//         if (!connectedPipes.Any())
//         {
//             return new List<Heatingnetworkiv>();
//         }
//
//         foreach (var connectedPipe in connectedPipes)
//         {
//             coords.Remove(connectedPipe.OgcFid);
//
//             result.Add(connectedPipe);
//             result.AddRange(await GetPipesAsync(coords, connectedPipe));
//         }
//
//         return result;
//     }
// }