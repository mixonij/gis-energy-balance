namespace Ispu.Gis.EnergyBalances;

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
    public static IServiceCollection AddWebApiServices(this IServiceCollection services)
    {
        // Добавляем контроллеры
        services.AddControllersWithViews();

        // Сваггер
        services.AddSwaggerDocument(settings =>
        {
            settings.PostProcess = document =>
            {
                document.Info.Version = "v1";
                document.Info.Title = "Energy Balances API";
                document.Info.Description = "REST API Energy Balances.";
            };
        });

        return services;
    }
}