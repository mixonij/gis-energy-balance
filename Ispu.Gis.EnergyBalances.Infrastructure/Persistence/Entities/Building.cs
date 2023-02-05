﻿using NpgsqlTypes;

namespace Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities;

public partial class Building
{
    public int Id { get; set; }

    public int CityId { get; set; }

    public NpgsqlPoint Coordinates { get; set; }

    public NpgsqlPoint[] PolygonCoordinates { get; set; } = null!;

    public virtual ICollection<BuildingsInfo> BuildingsInfos { get; } = new List<BuildingsInfo>();

    public virtual City City { get; set; } = null!;
}
