namespace Ispu.Gis.EnergyBalances.Domain.Entities;

public class HeatingStation : IEntity
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
    public Point[] Points { get; set; } = Array.Empty<Point>();
}