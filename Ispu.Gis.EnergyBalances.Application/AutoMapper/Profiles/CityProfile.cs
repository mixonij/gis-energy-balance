using AutoMapper;
using NetTopologySuite.Geometries;
using NpgsqlTypes;
using City = Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities.City;
using CityDistrict = Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities.CityDistrict;
using Point = Ispu.Gis.EnergyBalances.Domain.Entities.Point;

namespace Ispu.Gis.EnergyBalances.Application.AutoMapper.Profiles;

public class CityProfile : Profile
{
    /// <summary>
    /// Профиль маппингов для данных
    /// </summary>
    public CityProfile()
    {
        CreateMap<NpgsqlPoint, Coordinate>();
        CreateMap<City, Domain.Entities.City>().ForMember(x => x.NorthWestPoint,
                expression => expression.MapFrom(s => new Point(s.NorthWestPoint.X, s.NorthWestPoint.Y)))
            .ForMember(x => x.SouthEastPoint,
                expression => expression.MapFrom(s => new Point(s.SouthEastPoint.X, s.SouthEastPoint.Y)));
        CreateMap<CityDistrict, Domain.Entities.CityDistrict>().ForMember(x => x.GeometryPoints,
            expression => expression.MapFrom(s => s.Geometry.Boundary.Coordinates));
    }
}