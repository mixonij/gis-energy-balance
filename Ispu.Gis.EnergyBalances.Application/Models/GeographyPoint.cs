using NetTopologySuite.Geometries;

namespace Ispu.Gis.EnergyBalances.Application.Models;

public class GeographyPoint
{
    public GeographyPoint(Coordinate coordinate)
    {
        X = coordinate.X;
        Y = coordinate.Y;
    }
    
    public double X { get; set; }
    
    public double Y { get; set; }

}