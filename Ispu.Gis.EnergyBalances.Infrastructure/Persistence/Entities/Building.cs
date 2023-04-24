using NetTopologySuite.Geometries;
using NpgsqlTypes;

namespace Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities;

/// <summary>
/// Здание
/// </summary>
public class Building
{
    /// <summary>
    /// Идентификатор здания
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор города
    /// </summary>
    public int? CityId { get; set; }

    /// <summary>
    /// Идентификатор района
    /// </summary>
    public int? DistrictId { get; set; }
    
    /// <summary>
    /// Геометрия
    /// </summary>
    public Polygon Geometry { get; set; }

    /// <summary>
    /// Город
    /// </summary>
    public City? City { get; set; }
    
    /// <summary>
    /// Городской район
    /// </summary>
    public CityDistrict? CityDistrict { get; set; }
    
    /// <summary>
    /// Информация о здании
    /// </summary>
    public BuildingInfo? BuildingInfo { get; set; } 
}
