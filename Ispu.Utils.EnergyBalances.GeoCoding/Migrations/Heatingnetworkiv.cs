using System;
using System.Collections.Generic;
using NetTopologySuite.Geometries;

namespace Ispu.Utils.EnergyBalances.GeoCoding.Migrations;

public partial class Heatingnetworkiv
{
    public int OgcFid { get; set; }

    public MultiLineString? WkbGeometry { get; set; }

    public decimal? Objectid { get; set; }

    public decimal? Sys { get; set; }

    public decimal? Dpod { get; set; }

    public decimal? Dobr { get; set; }

    public decimal? ShapeLeng { get; set; }
}
