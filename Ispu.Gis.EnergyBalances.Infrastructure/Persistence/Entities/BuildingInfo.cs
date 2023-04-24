namespace Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities;

/// <summary>
/// Информация о здании
/// </summary>
public class BuildingInfo
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор здания
    /// </summary>
    public int BuildingId { get; set; }

    /// <summary>
    /// Год постройки
    /// </summary>
    public int? BuiltYear { get; set; }

    /// <summary>
    /// Число жильцов
    /// </summary>
    public int ResidentsCount { get; set; }

    /// <summary>
    /// Жилая площадь здания
    /// </summary>
    public double Area { get; set; }
    
    /// <summary>
    /// Ключ строки из реестра
    /// </summary>
    public int RegistryId { get; set; }

    /// <summary>
    /// Здание
    /// </summary>
    public Building Building { get; set; } = null!;
}
