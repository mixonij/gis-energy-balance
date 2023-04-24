using NetTopologySuite.Geometries;

namespace Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities;

/// <summary>
/// Городской район
/// </summary>
public class CityDistrict
{
    /// <summary>
    /// Идентификатор района
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор города
    /// </summary>
    public int CityId { get; set; }
    
    /// <summary>
    /// Идентификатор тепловой станции
    /// </summary>
    public int? HeatingStationId { get; set; }

    /// <summary>
    /// Геометрия
    /// </summary>
    public Polygon Geometry { get; set; }
    
    /// <summary>
    /// Город
    /// </summary>
    public City? City { get; set; }
    
    /// <summary>
    /// Тепловая станция
    /// </summary>
    public HeatingStation? HeatingStation { get; set; }

    /// <summary>
    /// Здания района
    /// </summary>
    public List<Building> Buildings { get; set; } = new();
}
