namespace Ispu.Gis.EnergyBalances.Domain.Entities;

/// <summary>
/// Сущность с полигоном
/// </summary>
public interface IPolygonEntity : IEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Координаты полигона
    /// </summary>
    Point[] GeometryPoints { get; set; }

    /// <summary>
    /// Центр полигона
    /// </summary>
    public Point Center { get; set; }
}