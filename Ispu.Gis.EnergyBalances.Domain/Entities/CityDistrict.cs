namespace Ispu.Gis.EnergyBalances.Domain.Entities;

/// <summary>
/// Сущность городского района
/// </summary>
public class CityDistrict: IPolygonEntity
{
    /// <summary>
    /// Идентификатор района
    /// </summary>
    public int Id { get; set; }

    public Point Center { get; set; }

    /// <summary>
    /// Идентификатор города
    /// </summary>
    public int CityId { get; set; }

    /// <summary>
    /// Точки геометрии
    /// </summary>
    public Point[] GeometryPoints { get; set; } = Array.Empty<Point>();

    /// <summary>
    /// Здания
    /// </summary>
    public Building[] Buildings { get; set; } = Array.Empty<Building>();
}