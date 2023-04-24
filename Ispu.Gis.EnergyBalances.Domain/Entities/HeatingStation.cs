namespace Ispu.Gis.EnergyBalances.Domain.Entities;

public class HeatingStation : IPolygonEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Центр
    /// </summary>
    public Point Center { get; set; }

    /// <summary>
    /// Идентификатор города
    /// </summary>
    public int CityId { get; set; }

    /// <summary>
    /// Номинальная мощность
    /// </summary>
    public double NominalPower { get; set; }
    
    /// <summary>
    /// Наименование
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Геометрия
    /// </summary>
    public Point[] GeometryPoints { get; set; } = Array.Empty<Point>();
}