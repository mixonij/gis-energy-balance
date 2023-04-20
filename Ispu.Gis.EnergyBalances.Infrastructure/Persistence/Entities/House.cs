using NpgsqlTypes;

namespace Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities;

public partial class House
{
    public int Id { get; set; }

    public NpgsqlPoint Coords { get; set; }

    public string? Description { get; set; }

    public string Address { get; set; } = null!;
}
