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
    /// Геометрия
    /// </summary>
    public MultiLineString Geometry { get; set; }
    
    /// <summary>
    /// Город
    /// </summary>
    public City? City { get; set; }
}
