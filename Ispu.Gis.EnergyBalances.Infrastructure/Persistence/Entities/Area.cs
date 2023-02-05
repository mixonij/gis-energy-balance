using NpgsqlTypes;

namespace Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities;

public partial class Area
{
    public int Id { get; set; }

    public int CityId { get; set; }

    public NpgsqlPoint[] PolygonCoordinates { get; set; } = null!;
}
