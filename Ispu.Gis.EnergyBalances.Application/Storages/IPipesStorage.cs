using Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities;

namespace Ispu.Gis.EnergyBalances.Application.Storages;

public interface IPipesStorage
{
    List<IEnumerable<Heatingnetworkiv>> GetAllPipes();
    Task Initialize();
    Task<List<Heatingnetworkiv>> GetPipes();
}