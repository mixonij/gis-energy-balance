using NetTopologySuite.Geometries;

namespace Ispu.Gis.EnergyBalances.Application.Models;

public class Pipe
{
    public int Id { get; set; }

    public ICollection<GeographyPoint> Points { get; set; }

    public decimal? DOut { get; set; }

    public decimal? DIn { get; set; }

    public decimal? Length { get; set; }
}