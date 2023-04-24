using NpgsqlTypes;

namespace Ispu.Gis.EnergyBalances.Infrastructure.Persistence.Entities;

/// <summary>
/// Модель города
/// </summary>
public class City
{
    /// <summary>
    /// Идентификатор города
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Имя на английском языке
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Имя на русском языке
    /// </summary>
    public string NativeName { get; set; } = null!;

    /// <summary>
    /// Северо-западная точка границы области отображения
    /// </summary>
    public NpgsqlPoint NorthWestPoint { get; set; }

    /// <summary>
    /// Юго-восточная точка границы области отображения
    /// </summary>
    public NpgsqlPoint SouthEastPoint { get; set; }

    /// <summary>
    /// Минимальный зум области отображения
    /// </summary>
    public int MinZoom { get; set; }

    /// <summary>
    /// Районы города
    /// </summary>
    public List<CityDistrict> CityDistricts { get; set; } = new();

    /// <summary>
    /// Здания города
    /// </summary>
    public List<Building> Buildings { get; set; } = new();

    /// <summary>
    /// Тепловые станции
    /// </summary>
    public List<HeatingStation> HeatingStations { get; set; } = new();
}