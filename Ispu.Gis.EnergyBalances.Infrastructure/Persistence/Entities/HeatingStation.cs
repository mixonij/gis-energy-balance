using NpgsqlTypes;

namespace Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities;

public partial class HeatingStation
{
    public int Id { get; set; }

    public int CityId { get; set; }

    public float NominalPower { get; set; }

    public NpgsqlPoint Coords { get; set; }

    public virtual City City { get; set; } = null!;
}
