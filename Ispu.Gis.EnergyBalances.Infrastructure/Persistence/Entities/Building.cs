using NpgsqlTypes;

namespace Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities;

public sealed class Building
{
    public int Id { get; set; }

    public int CityId { get; set; }

    public NpgsqlPoint Coordinates { get; set; }

    public City City { get; set; } = null!;
}
