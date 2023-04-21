using Ispu.Gis.EnergyBalances.Application.AutoMapper.Profiles;

namespace Ispu.Gis.EnergyBalances.Application.AutoMapper.Configurations;

/// <summary>
/// Конфигурация маппингов
/// </summary>
public class AutoMapperConfig
{
    /// <summary>
    /// Регистрация профилей маппингов
    /// </summary>
    /// <returns></returns>
    public static Type[] RegisterMappings()
    {
        return new[]
        {
            typeof(CityProfile)
        };
    }
}