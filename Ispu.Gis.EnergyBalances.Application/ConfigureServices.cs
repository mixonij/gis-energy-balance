using Ispu.Gis.EnergyBalances.Application.AutoMapper.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace Ispu.Gis.EnergyBalances.Application;

/// <summary>
/// Конфигуратор сервисов
/// </summary>
public static class ConfigureServices
{
    /// <summary>
    /// Конфигурация
    /// </summary>
    /// <param name="services">Коллекция сервисов</param>
    /// <returns>Коллекция сервисов</returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(AutoMapperConfig.RegisterMappings());
        return services;
    }
}