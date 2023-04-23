namespace Ispu.Gis.EnergyBalances.Domain.Entities;

public class Building: IEntity
{
    public double LivingSquare { get; set; }
    
    public int ResidentsCount { get; set; }

    public double AOk { get; set; } = 1723;

    public double AF { get; set; } = 251;

    public double V => LivingSquare * 2.5;
    
    public int Id { get; set; }
    
    /// <summary>
    /// Точки геометрии
    /// </summary>
    public Point[] GeometryPoints { get; set; } = Array.Empty<Point>();
}