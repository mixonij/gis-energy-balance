using NpgsqlTypes;

namespace Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities;

public partial class City
{
    public int Id { get; set; }

    public string NameRussian { get; set; } = null!;

    public NpgsqlPoint NorthWestBound { get; set; }

    public NpgsqlPoint SouthEastBound { get; set; }

    public int MinZoom { get; set; }

    public virtual ICollection<Building> Buildings { get; } = new List<Building>();
}
