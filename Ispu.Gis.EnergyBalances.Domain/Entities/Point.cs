namespace Ispu.Gis.EnergyBalances.Domain.Entities;

/// <summary>
/// Точка
/// </summary>
public class Point
{
    /// <summary>
    /// Точка
    /// </summary>
    /// <param name="x">X</param>
    /// <param name="y">Y</param>
    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }
    
    /// <summary>
    /// X
    /// </summary>
    public double X { get;  }
    
    /// <summary>
    /// Y
    /// </summary>
    public double Y { get; }
}