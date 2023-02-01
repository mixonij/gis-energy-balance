namespace Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities;

public sealed partial class City
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? NameRussian { get; set; }

    public ICollection<Building> Buildings { get; } = new List<Building>();
}
