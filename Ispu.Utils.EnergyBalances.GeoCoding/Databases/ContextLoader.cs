using Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Ispu.Utils.EnergyBalances.GeoCoding.Databases;

/// <summary>
/// Загрузчик контекста БД
/// </summary>
public class ContextLoader
{
    /// <summary>
    /// Получение контекста
    /// </summary>
    /// <param name="connectionString">Строка подключения</param>
    /// <returns>Контекст БД</returns>
    public static async Task<CityEnergyModelingContext> GetContextAsync(string connectionString)
    {
        var options = new DbContextOptionsBuilder<CityEnergyModelingContext>()
            .UseNpgsql(connectionString, options => options.UseNetTopologySuite()).Options;
        var dbContext = new CityEnergyModelingContext(options);
        await dbContext.Database.MigrateAsync();

        return dbContext;
    }
}