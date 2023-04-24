using NetTopologySuite.Geometries;

namespace Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities;

public class HeatingStation
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор города
    /// </summary>
    public int CityId { get; set; }

    /// <summary>
    /// Номинальная мощность
    /// </summary>
    public double NominalPower { get; set; }

    /// <summary>
    /// Геометрия
    /// </summary>
    public Polygon Geometry { get; set; }

    /// <summary>
    /// Город
    /// </summary>
    public City City { get; set; } = null!;

    /// <summary>
    /// Городские районы
    /// </summary>
    public List<CityDistrict> CityDistricts { get; set; } = new();
}
