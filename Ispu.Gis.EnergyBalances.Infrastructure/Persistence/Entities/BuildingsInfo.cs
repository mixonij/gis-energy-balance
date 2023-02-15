namespace Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities;

public partial class BuildingsInfo
{
    public int Id { get; set; }

    public int BuildingId { get; set; }

    public int? BuiltYear { get; set; }

    public int ResidentsCount { get; set; }

    public float Area { get; set; }

    public virtual Building Building { get; set; } = null!;
}
