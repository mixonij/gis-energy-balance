namespace Ispu.Gis.EnergyBalances.Domain.Entities;

/// <summary>
/// Город
/// </summary>
public class City: IEntity
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
    public Point NorthWestPoint { get; set; }

    /// <summary>
    /// Юго-восточная точка границы области отображения
    /// </summary>
    public Point SouthEastPoint { get; set; }

    /// <summary>
    /// Минимальный зум области отображения
    /// </summary>
    public int MinZoom { get; set; }

    /// <summary>
    /// Районы города
    /// </summary>
    public List<CityDistrict> CityDistricts { get; set; } = new();
}