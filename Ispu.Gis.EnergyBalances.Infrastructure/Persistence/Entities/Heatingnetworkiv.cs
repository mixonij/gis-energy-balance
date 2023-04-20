using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities;

public partial class Heatingnetworkiv
{
    public Heatingnetworkiv()
    {
        
    }

    public Heatingnetworkiv(Heatingnetworkiv pipe)
    {
        OgcFid = pipe.OgcFid;
        WkbGeometry = EF.Functions.Transform(pipe.WkbGeometry!, 4326);
        Objectid = pipe.Objectid;
        Sys = pipe.Sys;
        Dpod = pipe.Dpod;
        Dobr = pipe.Dobr;
        ShapeLeng = pipe.ShapeLeng;
    }
    
    public int OgcFid { get; set; }

    public MultiLineString? WkbGeometry { get; set; }

    public decimal? Objectid { get; set; }

    public decimal? Sys { get; set; }

    public decimal? Dpod { get; set; }

    public decimal? Dobr { get; set; }

    public decimal? ShapeLeng { get; set; }
}
