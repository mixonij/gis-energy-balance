namespace Ispu.Gis.EnergyBalances.Domain.Entities;

/// <summary>
/// Тепловая труба
/// </summary>
public class HeatingPipe: IEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Координаты точек геометрии
    /// </summary>
    public Point[] Points { get; set; } = Array.Empty<Point>();

    /// <summary>
    /// Диаметр подающей трубы
    /// </summary>
    public decimal DPod { get; set; }

    /// <summary>
    /// Диаметр отводящей трубы
    /// </summary>
    public decimal DObr { get; set; }
}